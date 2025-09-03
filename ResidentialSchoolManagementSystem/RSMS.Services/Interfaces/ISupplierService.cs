using RSMS.Data.Models.LookupEntities;

namespace RSMS.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> AddAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);
        Task<bool> DeleteAsync(int id);
    }
}
