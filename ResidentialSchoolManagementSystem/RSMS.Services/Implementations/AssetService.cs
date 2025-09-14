using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDTO>> GetAllAsync()
        {
            var assets = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDTO>>(assets);
        }

        public async Task<AssetDTO?> GetByIdAsync(Guid id)
        {
            var asset = await _repository.GetByIdAsync(id);
            return asset == null ? null : _mapper.Map<AssetDTO>(asset);
        }

        public async Task<AssetDTO> AddAsync(AssetDTO dto)
        {
            var asset = _mapper.Map<AssetIssue>(dto);
            var created = await _repository.AddAsync(asset);
            return _mapper.Map<AssetDTO>(created);
        }

        public async Task<AssetDTO> UpdateAsync(AssetDTO dto)
        {
            var asset = _mapper.Map<AssetIssue>(dto);
            var updated = await _repository.UpdateAsync(asset);
            return _mapper.Map<AssetDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
