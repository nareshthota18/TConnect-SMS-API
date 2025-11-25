using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Common.DTO;
using RSMS.Common;

namespace RSMS.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly RSMSDbContext _context;

        public UserRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(Guid rSHostelId)
        {
            // Find all Users where ANY of their related UserHostels matches the provided RSHostelId.
            if (rSHostelId == Guid.Empty)
            {
                return await _context.Users.ToListAsync();
            }
            else
            {
                return await _context.Users
               .Where(user => user.UserHostels.Any(uh => uh.RSHostelId == rSHostelId))
               .ToListAsync();
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User user)
        {
            // Ensure new Guid is generated for the user
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task AddAUserRolesync(UserHostel role)
        {
            _context.UserHostels.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserRolesync(UserHostel role)
        {
            _context.UserHostels.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            var userRole = await _context.UserHostels.FirstOrDefaultAsync(u => u.UserId == id);
            if (userRole != null)
            {
                _context.UserHostels.Remove(userRole);
                await _context.SaveChangesAsync();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidUser(string userName, string password)
        {
            //  This is plain-text matching. Replace with hashing in real use.
            //return await _context.Users.AnyAsync(u => u.Username == userName && u.PasswordHash == password);
            bool isValid = false;
            var exuser = _context.Users.FirstOrDefault(u => u.Username == userName || u.Email == userName);
            if (exuser != null)
            {
                isValid = GeneratePasswordHash.VerifyPassword(password, exuser.PasswordHash, exuser.PasswordSalt);
            }
            return isValid;
        }

        public async Task<bool> UpdatePassword(ResetPassword user, byte[] PasswordHash, byte[] PasswordSalt)
        {
            var exuser = _context.Users.FirstOrDefault(u => u.Username == user.Username || u.Email == user.Username);
            if (exuser != null)
            {
                exuser.PasswordHash = PasswordHash;
                exuser.PasswordSalt = PasswordSalt;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetRoleByUserAsync(string usernameOrEmail)
        {
            var roleName = await _context.Users
        .Where(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail)
        .Join(_context.UserHostels,
              user => user.Id,
              userRole => userRole.UserId,
              (user, userRole) => new { user, userRole })
        .Join(_context.Roles,
              ur => ur.userRole.RoleId,
              role => role.Id,
              (ur, role) => role.Name)
        .FirstOrDefaultAsync();

            return roleName ?? string.Empty;
        }

        public async Task<User?> GetByuserSchoolIdAsync(string userName, Guid schoolId)
        {
            // Get the user first
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
            if (user == null)
                return null;

            // Check if the user belongs to the given school/hostel
            var isInSchool = await _context.UserHostels
                .AnyAsync(uh => uh.UserId == user.Id && uh.RSHostelId == schoolId); // or SchoolId

            return isInSchool ? user : null;
        }

        public async Task<User?> GetByuserAsync(string userName)
        {
            // Get the user first
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
            if (user == null)
                return null;

            return user;
        }

        public async Task<List<UserHostel>> GetUserHostelsAsync(Guid userId)
        {
            var userHostels = await _context.UserHostels
                                .Include(uh => uh.RSHostel)
                                .Include(uh => uh.Role)
                                .Where(uh => uh.UserId == userId && uh.RSHostel.IsActive)
                                .OrderByDescending(uh => uh.IsPrimary)
                                .ToListAsync();


            return userHostels;
        }

    }
}
