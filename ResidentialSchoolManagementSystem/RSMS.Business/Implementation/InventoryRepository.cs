using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        public InventoryRepository(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }
        public Task<ItemDTO> AddItemAsync(ItemDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemDTO?> GetItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDTO> UpdateItemAsync(ItemDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
