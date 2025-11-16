using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.LookupEntities;
using RSMS.Repositories.Contracts;

namespace RSMS.Repositories.Implementation
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly RSMSDbContext _context;

        public SchoolRepository(RSMSDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RSHostel hostel)
        {
            hostel.Id = Guid.NewGuid();
            _context.RSHostels.Add(hostel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RSHostel>> GetAllAsync(Guid UserId)
        {
            return await _context.RSHostels.Where(x => x.CreatedBy == UserId).ToListAsync();
        }
        public async Task<IEnumerable<RSHostel>> GetAllAsync()
        {
            return await _context.RSHostels.Where(x => x.IsActive).ToListAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var sc = await _context.RSHostels.FindAsync(id);
            if (sc == null) return false;

            sc.IsActive = false;
            _context.RSHostels.Update(sc);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.RSHostels
                .AnyAsync(h => h.Name.ToLower() == name.ToLower());
        }

    }
}
