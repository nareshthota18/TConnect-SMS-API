using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(Guid id);
        Task<Item> AddAsync(Item item);
        Task<Item> UpdateAsync(Item item);
        Task<bool> DeleteAsync(Guid id);
    }
}
