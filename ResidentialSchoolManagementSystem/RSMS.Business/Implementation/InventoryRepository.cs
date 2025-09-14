﻿using Microsoft.EntityFrameworkCore;
using RSMS.Business.Contracts;
using RSMS.Data;
using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Services.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly RSMSDbContext _context;

        public InventoryRepository(RSMSDbContext context)
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

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
