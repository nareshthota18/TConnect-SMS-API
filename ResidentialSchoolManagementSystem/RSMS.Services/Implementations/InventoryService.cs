using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDTO>>(entities);
        }

        public async Task<ItemDTO?> GetItemByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ItemDTO>(entity);
        }

        public async Task<ItemDTO> AddItemAsync(ItemDTO item)
        {
            var entity = _mapper.Map<Item>(item);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<ItemDTO>(created);
        }

        public async Task<ItemDTO> UpdateItemAsync(ItemDTO item)
        {
            var entity = _mapper.Map<Item>(item);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ItemDTO>(updated);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync()
        {
            var assets = await _repository.GetItemTypesAsync();
            return _mapper.Map<IEnumerable<ItemTypeDTO>>(assets);
        }
    }
}
