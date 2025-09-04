using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IInventoryManager
    {
        Task<ItemDTO?> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO> AddItemAsync(ItemDTO item);
        Task<ItemDTO> UpdateItemAsync(ItemDTO item);
        Task<bool> DeleteItemAsync(int id);
    }
}
