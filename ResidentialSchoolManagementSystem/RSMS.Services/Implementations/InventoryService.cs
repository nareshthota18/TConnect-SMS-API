using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly RsmsDbContext _context;

        public InventoryService(RsmsDbContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetItemByIdAsync(int id) =>
            await _context.Items.Include(i => i.ItemType).FirstOrDefaultAsync(i => i.ItemId == id);

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await _context.Items.Include(i => i.ItemType).ToListAsync();

        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
