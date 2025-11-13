using Microsoft.EntityFrameworkCore;
using RSMS.Common.DTO;
using RSMS.Data;
using RSMS.Repositories.Contracts;
using System.Linq.Expressions;

namespace RSMS.Repositories.Implementation
{
    public class LookupRepository<TEntity, TKey> : ILookupRepository<TEntity, TKey> where TEntity : class
    {
        private readonly RSMSDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public LookupRepository(RSMSDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector)
        {
            return await _dbSet
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id, typeof(TKey));
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await _dbSet.AnyAsync(lambda);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

       
        public async Task<bool> ExistsByNameAsync(string name)
        {
            var nameProp = typeof(TEntity).GetProperty("Name");
            if (nameProp == null)
                throw new InvalidOperationException($"{typeof(TEntity).Name} must have a Name property.");

            return await _dbSet.AnyAsync(e =>
                EF.Functions.Like(EF.Property<string>(e, "Name").ToLower(), name.ToLower()));
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

      
        public async Task<bool> DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            var isActiveProp = typeof(TEntity).GetProperty("IsActive");
            if (isActiveProp != null)
            {
                // Soft delete
                isActiveProp.SetValue(entity, false);
                _dbSet.Update(entity);
            }
            else
            {
                // Hard delete
                _dbSet.Remove(entity);
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
