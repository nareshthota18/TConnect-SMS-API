using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetIssueDTO>> GetAllAsync();
        Task<AssetIssueDTO?> GetByIdAsync(Guid id);
        Task<AssetIssueDTO> AddAsync(AssetIssueDTO asset);
        Task<AssetIssueDTO> UpdateAsync(AssetIssueDTO asset);
        Task<bool> DeleteAsync(Guid id);
    }
}
