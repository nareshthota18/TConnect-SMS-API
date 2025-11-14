using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Implementation;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class SchoolActivityService : ISchoolActivityService
    {
        private readonly ISchoolActivityRepository _repo;
        private readonly IMapper _mapper;

        public SchoolActivityService(ISchoolActivityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(SchoolActivityDTO dto, Guid userId)
        {
            var entity = _mapper.Map<SchoolActivity>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, SchoolActivityDTO dto, Guid userId)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Category = dto.Category;
            entity.ActivityDate = dto.ActivityDate;
            entity.RSHostelId = dto.HostelId;

            entity.UpdatedBy = userId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<SchoolActivityDTO>> GetByHostelAsync(Guid hostelId)
        {
            var list = await _repo.GetByHostelAsync(hostelId);
            return _mapper.Map<List<SchoolActivityDTO>>(list);
        }

        public async Task<SchoolActivityDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<SchoolActivityDTO>(entity);
        }
    }

}
