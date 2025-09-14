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

        public async Task<IEnumerable<AssetIssueDTO>> GetAllAsync()
        {
            var assets = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetIssueDTO>>(assets);
        }

        public async Task<AssetIssueDTO?> GetByIdAsync(Guid id)
        {
            var asset = await _repository.GetByIdAsync(id);
            return asset == null ? null : _mapper.Map<AssetIssueDTO>(asset);
        }

        public async Task<AssetIssueDTO> AddAsync(AssetIssueDTO dto)
        {
            var asset = _mapper.Map<AssetIssue>(dto);
            var created = await _repository.AddAsync(asset);
            return _mapper.Map<AssetIssueDTO>(created);
        }

        public async Task<AssetIssueDTO> UpdateAsync(AssetIssueDTO dto)
        {
            var asset = _mapper.Map<AssetIssue>(dto);
            var updated = await _repository.UpdateAsync(asset);
            return _mapper.Map<AssetIssueDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
