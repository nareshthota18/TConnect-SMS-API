using Microsoft.EntityFrameworkCore;
using RSMS.Common.Models;
using RSMS.Data;
using RSMS.Repositories.Contracts;
using System.Linq.Expressions;

namespace RSMS.Repositories.Implementation
{
    public class LookupRepository<TEntity, TKey> : ILookupRepository<TEntity, TKey> where TEntity : class
    {
        private readonly RSMSDbContext _context;

        public LookupRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id, typeof(TKey));
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await _context.Set<TEntity>().AnyAsync(lambda);
        }

        public async Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector)
        {
            return await _context.Set<TEntity>()
                                 .AsNoTracking()
                                 .Select(selector)
                                 .ToListAsync();
        }
    }
}
