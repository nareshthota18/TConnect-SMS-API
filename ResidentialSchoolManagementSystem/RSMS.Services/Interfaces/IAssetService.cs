using RSMS.Common.Models;

namespace RSMS.Services.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetDTO>> GetAllAsync();
        Task<AssetDTO?> GetByIdAsync(Guid id);
        Task<AssetDTO> AddAsync(AssetDTO asset);
        Task<AssetDTO> UpdateAsync(AssetDTO asset);
        Task<bool> DeleteAsync(Guid id);
    }
}
