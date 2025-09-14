using RSMS.Data.Models.LookupEntities;

namespace RSMS.Repositories.Contracts
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(Guid id);
        Task<Supplier> AddAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);
        Task<bool> DeleteAsync(Guid id);
    }
}
