using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            var entities = await _inventoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDTO>>(entities);
        }

        public async Task<ItemDTO?> GetItemByIdAsync(Guid id)
        {
            var entity = await _inventoryRepository.GetByIdAsync(id);
            return _mapper.Map<ItemDTO>(entity);
        }

        public async Task<ItemDTO> AddItemAsync(ItemDTO item)
        {
            var entity = _mapper.Map<Item>(item);
            var created = await _inventoryRepository.AddAsync(entity);
            return _mapper.Map<ItemDTO>(created);
        }

        public async Task<ItemDTO> UpdateItemAsync(ItemDTO item)
        {
            var entity = _mapper.Map<Item>(item);
            var updated = await _inventoryRepository.UpdateAsync(entity);
            return _mapper.Map<ItemDTO>(updated);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            return await _inventoryRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync()
        {
            var assets = await _inventoryRepository.GetItemTypesAsync();
            return _mapper.Map<IEnumerable<ItemTypeDTO>>(assets);
        }
    }
}
