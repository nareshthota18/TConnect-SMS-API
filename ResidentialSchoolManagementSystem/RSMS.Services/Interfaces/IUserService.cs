using Azure.Core;
using RSMS.Common.DTO;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(Guid id);
        Task<UserDTO> AddAsync(UserDTO user);
        Task<UserDTO> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ValidUser(string userName, string password);
        Task<bool> UpdatePassword(ResetPassword user);
        Task<List<UserHostel?>> GetUserHostelsAsync(Guid userId, bool isSuperAdmin);
        Task<User?> GetByuserAsync(string username);
    }
}
