using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.LookupEntities;

namespace RSMS.Repositories.Implementation
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly RSMSDbContext _context;

        public SupplierRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
            => await _context.Suppliers.ToListAsync();

        public async Task<Supplier?> GetByIdAsync(Guid id)
            => await _context.Suppliers.FindAsync(id);

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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
