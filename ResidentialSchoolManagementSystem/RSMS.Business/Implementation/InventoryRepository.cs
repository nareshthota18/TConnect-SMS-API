using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Repositories.Contracts;

namespace RSMS.Repositories.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly RSMSDbContext _context;

        public InventoryRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllBySchoolAsync(Guid schoolId)
        {
            return await _context.Inventory
                .Include(i => i.Item)
                .ThenInclude(i => i.ItemType)
                .Where(i => i.RSHostelId == schoolId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAGroceryAsync(Guid schoolId)
        {
            //just filter data only Grocery type
            return await _context.Inventory
                .Include(i => i.Item)
                .ThenInclude(i => i.ItemType)
                .Where(i => i.RSHostelId == schoolId && i.ItemId == Guid.Parse("CC30BE0E-2F04-4FBF-96DC-26A3F380B377"))
                .ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(Guid id)
        {
            return await _context.Inventory
                .Include(i => i.Item)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Inventory> AddAsync(Inventory entity)
        {
            // Ensure no duplicate (same item and school)
            var exists = await _context.Inventory
                .AnyAsync(i => i.RSHostelId == entity.RSHostelId && i.ItemId == entity.ItemId);

            if (exists)
                throw new InvalidOperationException("Inventory record for this item already exists for the selected school.");

            _context.Inventory.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Inventory> UpdateAsync(Inventory entity)
        {
            var existing = await _context.Inventory.FirstOrDefaultAsync(i => i.Id == entity.Id);
            if (existing == null)
                throw new KeyNotFoundException("Inventory record not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            existing.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Inventory.FindAsync(id);
            if (entity == null) return false;

            _context.Inventory.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

