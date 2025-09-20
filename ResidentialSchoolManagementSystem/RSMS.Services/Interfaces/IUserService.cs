using RSMS.Common.Models;

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
    }
}
