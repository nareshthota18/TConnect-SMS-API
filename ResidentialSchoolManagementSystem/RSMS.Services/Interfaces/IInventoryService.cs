using RSMS.Common.Models;

namespace RSMS.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO?> GetItemByIdAsync(Guid id);
        Task<ItemDTO> AddItemAsync(ItemDTO item);
        Task<ItemDTO> UpdateItemAsync(ItemDTO item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync();
    }
}
