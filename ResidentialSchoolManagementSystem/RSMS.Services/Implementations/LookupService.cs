using RSMS.Common.Models;
using RSMS.Repositories.Contracts;
using RSMS.Services.Contracts;
using System.Linq.Expressions;

namespace RSMS.Services.Implementation
{
    public class LookupService<TEntity, TKey> : ILookupService<TEntity, TKey> where TEntity : class
    {
        private readonly ILookupRepository<TEntity, TKey> _lookupRepository;

        public LookupService(ILookupRepository<TEntity, TKey> lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        public async Task<List<LookupDTO>> GetLookupAsync(Expression<Func<TEntity, LookupDTO>> selector)
        {
            return await _lookupRepository.GetLookupAsync(selector);
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            return await _lookupRepository.ExistsAsync(id);
        }
    }
}
