using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Contracts
{
    public interface ILookupRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> ExistsAsync(TKey id);
    }
}
