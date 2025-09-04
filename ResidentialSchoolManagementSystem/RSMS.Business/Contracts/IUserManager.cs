using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;


namespace RSMS.Business.Contracts
{
    public interface IUserManager
    {
        Task<UserDTO?> GetByIdAsync(long id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> AddAsync(UserDTO user);
        Task<UserDTO> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(long id);
    }
}
