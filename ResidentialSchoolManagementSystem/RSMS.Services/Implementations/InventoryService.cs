using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryDTO>> GetAllBySchoolAsync(Guid schoolId)
        {
            var inventories = await _inventoryRepository.GetAllBySchoolAsync(schoolId);
            return _mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<InventoryDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _inventoryRepository.GetByIdAsync(id);
            return _mapper.Map<InventoryDTO?>(entity);
        }

        public async Task<InventoryDTO> AddAsync(InventoryDTO dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            var created = await _inventoryRepository.AddAsync(entity);
            return _mapper.Map<InventoryDTO>(created);
        }

        public async Task<InventoryDTO> UpdateAsync(InventoryDTO dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            var updated = await _inventoryRepository.UpdateAsync(entity);
            return _mapper.Map<InventoryDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _inventoryRepository.DeleteAsync(id);
        }
    }
}
