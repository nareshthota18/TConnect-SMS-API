using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.DTO;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repo;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Student Attendance
        public async Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync()
        {
            var list = await _repo.GetAllStudentAttendanceAsync();
            return _mapper.Map<IEnumerable<StudentAttendanceDTO>>(list);
        }

        public async Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(Guid id)
        {
            var att = await _repo.GetStudentAttendanceByIdAsync(id);
            return _mapper.Map<StudentAttendanceDTO?>(att);
        }

        public async Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO att)
        {
            var entity = _mapper.Map<StudentAttendance>(att);
            var created = await _repo.AddStudentAttendanceAsync(entity);
            return _mapper.Map<StudentAttendanceDTO>(created);
        }

        public async Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO att)
        {
            var entity = _mapper.Map<StudentAttendance>(att);
            var updated = await _repo.UpdateStudentAttendanceAsync(entity);
            return _mapper.Map<StudentAttendanceDTO>(updated);
        }

        public async Task<bool> DeleteStudentAttendanceAsync(Guid id) =>
            await _repo.DeleteStudentAttendanceAsync(id);

        // Staff Attendance
        public async Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync()
        {
            var list = await _repo.GetAllStaffAttendanceAsync();
            return _mapper.Map<IEnumerable<StaffAttendanceDTO>>(list);
        }

        public async Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(Guid id)
        {
            var att = await _repo.GetStaffAttendanceByIdAsync(id);
            return _mapper.Map<StaffAttendanceDTO?>(att);
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

        public async Task<bool> DeleteStaffAttendanceAsync(Guid id) =>
            await _repo.DeleteStaffAttendanceAsync(id);
    }
}
