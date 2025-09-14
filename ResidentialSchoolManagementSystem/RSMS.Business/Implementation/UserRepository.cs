using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly RSMSDbContext _context;

        public UserRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidUser(string userName, string password)
        {
            //  This is plain-text matching. Replace with hashing in real use.
            //return await _context.Users.AnyAsync(u => u.Username == userName && u.PasswordHash == password);
            return false;
        }
    }
}
