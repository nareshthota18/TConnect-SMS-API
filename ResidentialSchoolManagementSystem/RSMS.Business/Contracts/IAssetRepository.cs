using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IAssetRepository
    {
        Task<IEnumerable<AssetIssue>> GetAllAsync();
        Task<AssetIssue?> GetByIdAsync(Guid id);
        Task<AssetIssue> AddAsync(AssetIssue asset);
        Task<AssetIssue> UpdateAsync(AssetIssue asset);
        Task<bool> DeleteAsync(Guid id);
    }
}
