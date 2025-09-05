using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Services.Interfaces
{
    public interface IAssetService
    {
        Task<AssetIssue?> GetByIdAsync(Guid id);
        Task<IEnumerable<AssetIssue>> GetAllAsync();
        Task<AssetIssue> AddAsync(AssetIssue issue);
        Task<AssetIssue> UpdateAsync(AssetIssue issue);
        Task<bool> DeleteAsync(Guid id);
    }
}
