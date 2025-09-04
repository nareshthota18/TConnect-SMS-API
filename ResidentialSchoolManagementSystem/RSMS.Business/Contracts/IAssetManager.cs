using RSMS.Common.Models;

namespace RSMS.Business.Contracts
{
    public interface IAssetManager
    {
        Task<AssetDTO?> GetByIdAsync(long id);
        Task<IEnumerable<AssetDTO>> GetAllAsync();
        Task<AssetDTO> AddAsync(AssetDTO issue);
        Task<AssetDTO> UpdateAsync(AssetDTO issue);
        Task<bool> DeleteAsync(long id);
    }
}
