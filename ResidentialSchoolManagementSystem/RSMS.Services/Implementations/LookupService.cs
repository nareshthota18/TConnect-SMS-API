using RSMS.Common.DTO;
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

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _lookupRepository.GetByIdAsync(id);
        }

       
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _lookupRepository.ExistsByNameAsync(name);
        }

    
        public async Task AddAsync(TEntity entity)
        {
            await _lookupRepository.AddAsync(entity);
        }

       
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _lookupRepository.UpdateAsync(entity);
        }

       
        public async Task<bool> DeleteAsync(TKey id)
        {
            return await _lookupRepository.DeleteAsync(id);
        }
    }
}
