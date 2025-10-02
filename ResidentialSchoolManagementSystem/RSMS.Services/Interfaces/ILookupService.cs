using RSMS.Common.Models;
using System.Linq.Expressions;

namespace RSMS.Services.Contracts
{
    public interface ILookupService<TEntity, TKey> where TEntity : class
    {
        Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector);
        Task<bool> ExistsAsync(TKey id);
    }
}
