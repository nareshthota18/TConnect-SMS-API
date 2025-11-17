using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;

namespace RSMS.Services.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly RSMSDbContext _context;

        public ItemRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.Include(i => i.ItemType).ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await _context.Items.Include(i => i.ItemType).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Item> AddAsync(Item item)
        {
            var existingType = _context.ItemTypes.Where(x => x.Id == item.ItemTypeId).FirstOrDefault();
            if (existingType == null)
            {
                //item.ItemType = new ItemType()
                //{
                //    Id = Guid.NewGuid(),
                //    Name = item.Name
                //};
                //item.ItemTypeId = item.ItemType.Id;
            }
            else
            {
                item.ItemType = existingType; // Attach the loaded entity
                _context.Entry(item.ItemType).State = EntityState.Unchanged;
            }
            item.Id = Guid.NewGuid();
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;


            var assetIssue = await _context.AssetIssues.FirstOrDefaultAsync(u => u.ItemId == id);
            if (assetIssue != null)
            {
                _context.AssetIssues.Remove(assetIssue);
                await _context.SaveChangesAsync();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ItemType>> GetItemTypesAsync()
        {
            return await _context.ItemTypes.Include(i => i.Items).ToListAsync();
        }
    }
}
