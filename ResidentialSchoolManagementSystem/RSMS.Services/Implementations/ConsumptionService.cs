using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class ConsumptionService : IConsumptionService
    {
        private readonly IConsumptionRepository _repo;
        private readonly IMapper _mapper;

        public ConsumptionService(IConsumptionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddAsync(ConsumptionConfigDTO dto, Guid userId)
        {
            var entity = _mapper.Map<ConsumptionConfig>(dto);
            entity.CreatedBy = userId;

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(ConsumptionConfigDTO dto, Guid userId)
        {
            var entity = _mapper.Map<ConsumptionConfig>(dto);
            entity.UpdatedBy = userId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<ConsumptionConfigDTO?> GetByIdAsync(Guid id)
        {
            var obj = await _repo.GetByIdAsync(id);
            return obj == null ? null : _mapper.Map<ConsumptionConfigDTO>(obj);
        }

        public async Task<IEnumerable<ConsumptionConfigDTO>> GetAllAsync(Guid hostelId)
        {
            var data = await _repo.GetAllAsync(hostelId);
            return _mapper.Map<IEnumerable<ConsumptionConfigDTO>>(data);
        }
    }
}
