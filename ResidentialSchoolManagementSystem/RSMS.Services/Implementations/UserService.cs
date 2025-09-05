using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly RSMSDbContext _context;

        public UserService(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users
                .Include(u => u.Staff)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users
                .Include(u => u.Staff)
                .ToListAsync();

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
    }
}
