using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role?> GetByIdAsync(Guid id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> AddAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task<bool> DeleteAsync(Guid id);
    }
}
