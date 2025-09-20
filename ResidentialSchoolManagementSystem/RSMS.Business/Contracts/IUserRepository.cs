using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ValidUser(string userName, string password);
        Task<bool> UpdatePassword(ResetPassword user, byte[] PasswordHash, byte[] PasswordSalt);
    }
}
