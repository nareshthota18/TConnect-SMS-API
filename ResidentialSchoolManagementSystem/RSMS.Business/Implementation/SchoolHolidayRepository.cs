using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Contracts;

namespace RSMS.Repositories.Implementation
{
    public class SchoolHolidayRepository : ISchoolHolidayRepository
    {
        private readonly RSMSDbContext _context;

        public SchoolHolidayRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SchoolHoliday entity)
        {
            await _context.SchoolHolidays.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SchoolHoliday entity)
        {
            _context.SchoolHolidays.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _context.SchoolHolidays.FindAsync(id);
            if (obj == null) return false;

            _context.SchoolHolidays.Remove(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SchoolHoliday?> GetByIdAsync(Guid id)
        {
            return await _context.SchoolHolidays
                                 .Include(x => x.RSHostel)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SchoolHoliday>> GetBySchoolAsync(Guid hostelId)
        {
            return await _context.SchoolHolidays
                .Where(x => x.HostelId == hostelId)
                .OrderBy(x => x.StartDate)
                .ToListAsync();
        }
    }
}
