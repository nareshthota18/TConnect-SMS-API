using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<Item?> GetItemByIdAsync(Guid id);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(Guid id);
    }
}
