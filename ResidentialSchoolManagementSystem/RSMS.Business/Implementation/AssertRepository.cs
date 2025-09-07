using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Business.Implementation
{
    public class AssertRepository : IAssetRepository
    {
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        public AssertRepository(IAssetService assetService,IMapper mapper)
        {
            _assetService = assetService; 
            _mapper = mapper;
        }
        public async Task<AssetDTO> AddAsync(AssetDTO issue)
        {
            var asset = _mapper.Map<AssetIssue>(issue);
            var newAsset = await _assetService.AddAsync(asset);
            return _mapper.Map<AssetDTO>(newAsset);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _assetService.DeleteAsync(id);
        }

        public async Task<IEnumerable<AssetDTO>> GetAllAsync()
        {
            var assets = await _assetService.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDTO>>(assets);
        }

        public async Task<AssetDTO?> GetByIdAsync(Guid id)
        {
            var asset = await _assetService.GetByIdAsync(id);
            return _mapper.Map<AssetDTO>(asset);
        }

        public Task<AssetDTO> UpdateAsync(AssetDTO issue)
        {
            throw new NotImplementedException();
        }
    }
}
