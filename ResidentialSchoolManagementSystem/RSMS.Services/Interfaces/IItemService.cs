using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO?> GetItemByIdAsync(Guid id);
        Task<ItemDTO> AddItemAsync(ItemDTO item);
        Task<ItemDTO> UpdateItemAsync(ItemDTO item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync();
    }
}
