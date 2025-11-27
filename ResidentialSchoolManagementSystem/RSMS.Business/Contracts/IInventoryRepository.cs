using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllBySchoolAsync(Guid schoolId);
        Task<Inventory?> GetByIdAsync(Guid id);
        Task<Inventory> AddAsync(Inventory entity);
        Task<Inventory> UpdateAsync(Inventory entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Inventory>> GetAGroceryAsync(Guid schoolId);
    }

}
