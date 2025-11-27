using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSMS.Common.DTO;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConsumptionRepository _consumptionRepo;
        private readonly IInventoryRepository _inventoryRepo;
        private readonly RSMSDbContext _context;

        public AttendanceService(
            IAttendanceRepository repo,
            IMapper mapper,
            IConsumptionRepository consumptionRepo,
            IInventoryRepository inventoryRepo,
            RSMSDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _consumptionRepo = consumptionRepo;
            _inventoryRepo = inventoryRepo;
            _context = context;
        }

        // ---------------- STUDENT ATTENDANCE ----------------

        public async Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO att)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Map and save attendance
                var entity = _mapper.Map<StudentAttendance>(att);
                var created = await _repo.AddStudentAttendanceAsync(entity);

                // Update inventory
                await UpdateInventoryForStudentsAsync(new List<StudentAttendance> { entity });

                await transaction.CommitAsync();
                return _mapper.Map<StudentAttendanceDTO>(created);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<StudentAttendanceDTO>> CreateStudentAttendanceList(List<StudentAttendanceDTO> attList)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entities = _mapper.Map<List<StudentAttendance>>(attList);
                var savedList = await _repo.CreateStudentAttendanceList(entities);

                await UpdateInventoryForStudentsAsync(entities);

                await transaction.CommitAsync();
                return _mapper.Map<List<StudentAttendanceDTO>>(savedList);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task UpdateInventoryForStudentsAsync(List<StudentAttendance> attendances)
        {
            if (!attendances.Any()) return;

            // Load Student navigation to get GradeId & RSHostelId
            var studentIds = attendances.Select(a => a.StudentId).Distinct().ToList();
            var students = await _context.Students
                                         .Where(s => studentIds.Contains(s.Id))
                                         .ToDictionaryAsync(s => s.Id);

            // Group by hostel and grade
            var hostelGroups = attendances
                .Where(a => a.Status == "Present")
                .GroupBy(a => students[a.StudentId].RSHostelId);

            foreach (var hostelGroup in hostelGroups)
            {
                var hostelId = hostelGroup.Key;

                // Load inventory for hostel
                var inventoryDict = (await _inventoryRepo.GetAllBySchoolAsync(hostelId))
                    .ToDictionary(i => i.ItemId, i => i);

                // Load consumption configs for hostel
                var configs = await _consumptionRepo.GetAllAsync(hostelId);

                // Group by grade
                var gradeGroups = hostelGroup.GroupBy(a => students[a.StudentId].GradeId);

                foreach (var gradeGroup in gradeGroups)
                {
                    var gradeId = gradeGroup.Key;

                    // Active consumption configs for grade
                    var activeConfigs = configs
                        .Where(c => c.GradeId == gradeId
                                 && c.IsActive
                                 && c.EffectiveFrom.Date <= DateTime.UtcNow.Date
                                 && c.EffectiveTo.Date >= DateTime.UtcNow.Date);

                    foreach (var config in activeConfigs)
                    {
                        if (inventoryDict.TryGetValue(config.ItemId, out var inventory))
                        {
                            var totalQty = gradeGroup.Count() * config.Quantity;
                            inventory.QuantityIssued += totalQty;
                            inventory.LastUpdated = DateTime.UtcNow;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync(Guid RSHostelId)
        {
            var list = await _repo.GetAllStudentAttendanceAsync(RSHostelId);
            return _mapper.Map<IEnumerable<StudentAttendanceDTO>>(list);
        }

        public async Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(Guid id)
        {
            var entity = await _repo.GetStudentAttendanceByIdAsync(id);
            return _mapper.Map<StudentAttendanceDTO?>(entity);
        }

        public async Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO att)
        {
            var entity = _mapper.Map<StudentAttendance>(att);
            var updated = await _repo.UpdateStudentAttendanceAsync(entity);
            return _mapper.Map<StudentAttendanceDTO>(updated);
        }

        public async Task<bool> DeleteStudentAttendanceAsync(Guid id)
        {
            return await _repo.DeleteStudentAttendanceAsync(id);
        }

        // ---------------- STAFF ATTENDANCE ----------------

        public async Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync(Guid RSHostelId)
        {
            var list = await _repo.GetAllStaffAttendanceAsync(RSHostelId);
            return _mapper.Map<IEnumerable<StaffAttendanceDTO>>(list);
        }

        public async Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(Guid id)
        {
            var entity = await _repo.GetStaffAttendanceByIdAsync(id);
            return _mapper.Map<StaffAttendanceDTO?>(entity);
        }

        public async Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO att)
        {
            var entity = _mapper.Map<StaffAttendance>(att);
            var created = await _repo.AddStaffAttendanceAsync(entity);
            return _mapper.Map<StaffAttendanceDTO>(created);
        }

        public async Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO att)
        {
            var entity = _mapper.Map<StaffAttendance>(att);
            var updated = await _repo.UpdateStaffAttendanceAsync(entity);
            return _mapper.Map<StaffAttendanceDTO>(updated);
        }

        public async Task<bool> DeleteStaffAttendanceAsync(Guid id)
        {
            return await _repo.DeleteStaffAttendanceAsync(id);
        }

        public async Task<List<StaffAttendanceDTO>> CreateStaffAttendanceList(List<StaffAttendanceDTO> att)
        {
            var entities = _mapper.Map<List<StaffAttendance>>(att);
            var savedList = await _repo.CreateStaffAttendanceList(entities);
            return _mapper.Map<List<StaffAttendanceDTO>>(savedList);
        }
    }
}
