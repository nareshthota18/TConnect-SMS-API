using RSMS.Common.DTO;
using System.Linq.Expressions;

namespace RSMS.Services.Contracts
{
    public interface ILookupService<TEntity, TKey> where TEntity : class
    {
        Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector);
        Task<bool> ExistsAsync(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TKey id);
    }
}
