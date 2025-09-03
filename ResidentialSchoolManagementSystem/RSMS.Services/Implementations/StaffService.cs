using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly RsmsDbContext _context;

        public StaffService(RsmsDbContext context)
        {
            _context = context;
        }

        public async Task<Staff?> GetByIdAsync(long id) =>
            await _context.Staff
                .Include(s => s.Department)
                .Include(s => s.Designation)
                .FirstOrDefaultAsync(s => s.StaffId == id);

        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _context.Staff
                .Include(s => s.Department)
                .Include(s => s.Designation)
                .ToListAsync();

        public async Task<Staff> AddAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
