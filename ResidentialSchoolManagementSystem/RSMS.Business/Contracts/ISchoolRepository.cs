using RSMS.Data.Models.LookupEntities;

namespace RSMS.Repositories.Contracts
{
    public interface ISchoolRepository
    {
        Task AddAsync(RSHostel hostel);
        Task<IEnumerable<RSHostel>> GetAllAsync(Guid UserId);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<RSHostel>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
    }
}
