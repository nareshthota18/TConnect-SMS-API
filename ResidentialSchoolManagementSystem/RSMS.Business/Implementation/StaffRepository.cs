using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Repositories.Implementation
{
    public class StaffRepository : IStaffRepository
    {
        private readonly RSMSDbContext _context;
        private readonly IMapper _mapper;

        public StaffRepository(RSMSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllAsync(Guid RSHostelId)
        {
            var staffs = await _context.Staff.
                        Include(s => s.Designation)
                        .Include(s => s.Department).Where(s => s.RSHostelId == RSHostelId).ToListAsync();
            return _mapper.Map<IEnumerable<StaffDTO>>(staffs);
        }

        public async Task<StaffDTO?> GetByIdAsync(Guid id)
        {
            var staff = await _context.Staff.
                        Include(s => s.Designation)
     .Include(s => s.Department)
                        .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO> AddAsync(StaffDTO staffDto, Guid RSHostelId)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            staff.RSHostelId = RSHostelId;
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO> UpdateAsync(StaffDTO staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            var Staff = await _context.StaffAttendance.Where(u => u.StaffId == id).ToListAsync();
            if (Staff.Any())
            {
                _context.StaffAttendance.RemoveRange(Staff);
                await _context.SaveChangesAsync();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
