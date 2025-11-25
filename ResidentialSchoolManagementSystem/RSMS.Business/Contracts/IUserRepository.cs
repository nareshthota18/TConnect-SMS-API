using RSMS.Common.DTO;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(Guid rSHostelId);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ValidUser(string userName, string password);
        Task<bool> UpdatePassword(ResetPassword user, byte[] PasswordHash, byte[] PasswordSalt);
        Task<string> GetRoleByUserAsync(string usernameOrEmail);
        Task AddAUserRolesync(UserHostel role);
        Task UpdateUserRolesync(UserHostel role);
        Task<User> GetByuserSchoolIdAsync(string userName, Guid schoolId);
        Task<User> GetByuserAsync(string userName);
        Task<List<UserHostel?>> GetUserHostelsAsync(Guid userId);
    }
}
