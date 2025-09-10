using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IInventoryRepository
    {
        Task<ItemDTO?> GetItemByIdAsync(Guid id);
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO> AddItemAsync(ItemDTO item);
        Task<ItemDTO> UpdateItemAsync(ItemDTO item);
        Task<bool> DeleteItemAsync(Guid id);
    }
}
