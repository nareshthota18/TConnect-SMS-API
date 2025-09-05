﻿using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.LookupEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly RSMSDbContext _context;

        public SupplierService(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier?> GetByIdAsync(int id) =>
            await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);

        public async Task<IEnumerable<Supplier>> GetAllAsync() =>
            await _context.Suppliers.ToListAsync();

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
