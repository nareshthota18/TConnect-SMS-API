using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class InventoryManager : IInventoryManager
    {
        public Task<ItemDTO> AddItemAsync(ItemDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemDTO?> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDTO> UpdateItemAsync(ItemDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
