using Microsoft.EntityFrameworkCore;
using RSMS.Business.Contracts;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Business.Implementation
{
    public class AssetRepository : IAssetRepository
    {
        private readonly RSMSDbContext _context;

        public AssetRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AssetIssue>> GetAllAsync()
            => await _context.AssetIssues
                .Include(ai => ai.Student)
                .Include(ai => ai.Item)
                .ToListAsync();

        public async Task<AssetIssue?> GetByIdAsync(Guid id)
            => await _context.AssetIssues
                .Include(ai => ai.Student)
                .Include(ai => ai.Item)
                .FirstOrDefaultAsync(ai => ai.Id == id);

        public async Task<AssetIssue> AddAsync(AssetIssue asset)
        {
            _context.AssetIssues.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<AssetIssue> UpdateAsync(AssetIssue asset)
        {
            _context.AssetIssues.Update(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var asset = await _context.AssetIssues.FindAsync(id);
            if (asset == null) return false;

            _context.AssetIssues.Remove(asset);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
