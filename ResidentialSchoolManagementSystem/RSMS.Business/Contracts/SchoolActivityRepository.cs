using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Implementation;

namespace RSMS.Repositories.Contracts
{
    public class SchoolActivityRepository : ISchoolActivityRepository
    {
        private readonly RSMSDbContext _context;

        public SchoolActivityRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SchoolActivity entity)
        {
            await _context.SchoolActivities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SchoolActivity entity)
        {
            _context.SchoolActivities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<SchoolActivity?> GetByIdAsync(Guid id)
        {
            return await _context.SchoolActivities
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.SchoolActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SchoolActivity>> GetByHostelAsync(Guid hostelId)
        {
            return await _context.SchoolActivities
                .Where(x => x.RSHostelId == hostelId)
                .OrderByDescending(x => x.ActivityDate)
                .ToListAsync();
        }
    }

}
