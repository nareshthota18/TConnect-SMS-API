using RSMS.Business.Contracts;
using RSMS.Common.Models;

namespace RSMS.Business.Implementation
{
    public class AssetManager : IAssetManager
    {
        public Task<AssetDTO> AddAsync(AssetDTO issue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssetDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AssetDTO?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<AssetDTO> UpdateAsync(AssetDTO issue)
        {
            throw new NotImplementedException();
        }
    }
}
