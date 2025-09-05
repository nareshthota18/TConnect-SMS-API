using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class AssetService : IAssetService
    {
        private readonly RSMSDbContext _context;

        public AssetService(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<AssetIssue?> GetByIdAsync(Guid id) =>
            await _context.AssetIssues
                .Include(ai => ai.Student)
                .Include(ai => ai.Item)
                .FirstOrDefaultAsync(ai => ai.Id == id);

        public async Task<IEnumerable<AssetIssue>> GetAllAsync() =>
            await _context.AssetIssues
                .Include(ai => ai.Student)
                .Include(ai => ai.Item)
                .ToListAsync();

        public async Task<AssetIssue> AddAsync(AssetIssue issue)
        {
            _context.AssetIssues.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<AssetIssue> UpdateAsync(AssetIssue issue)
        {
            _context.AssetIssues.Update(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var issue = await _context.AssetIssues.FindAsync(id);
            if (issue == null) return false;

            _context.AssetIssues.Remove(issue);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
