using RSMS.Common.Models;

namespace RSMS.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(Guid id);
        Task<RoleDTO> AddAsync(RoleDTO role);
        Task<RoleDTO> UpdateAsync(RoleDTO role);
        Task<bool> DeleteAsync(Guid id);
    }
}
