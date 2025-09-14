using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Business.Contracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(Guid id);
        Task<Role> AddAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task<bool> DeleteAsync(Guid id);
    }
}
