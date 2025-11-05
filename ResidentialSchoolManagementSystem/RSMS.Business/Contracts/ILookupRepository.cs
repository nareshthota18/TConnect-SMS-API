using RSMS.Common.DTO;
using System.Linq.Expressions;

namespace RSMS.Repositories.Contracts
{
    public interface ILookupRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> ExistsAsync(TKey id);
        Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector);

    }
}
