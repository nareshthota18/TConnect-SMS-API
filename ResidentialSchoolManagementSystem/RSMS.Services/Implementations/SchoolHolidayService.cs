using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class SchoolHolidayService : ISchoolHolidayService
    {
        private readonly ISchoolHolidayRepository _repo;
        private readonly IMapper _mapper;

        public SchoolHolidayService(ISchoolHolidayRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddAsync(SchoolHolidayDTO dto, Guid userId)
        {
            var entity = _mapper.Map<SchoolHoliday>(dto);
            entity.CreatedBy = userId;

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(SchoolHolidayDTO dto, Guid userId)
        {
            var entity = _mapper.Map<SchoolHoliday>(dto);
            entity.UpdatedBy = userId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<SchoolHolidayDTO?> GetByIdAsync(Guid id)
        {
            var obj = await _repo.GetByIdAsync(id);
            return obj == null ? null : _mapper.Map<SchoolHolidayDTO>(obj);
        }

        public async Task<IEnumerable<SchoolHolidayDTO>> GetBySchoolAsync(Guid hostelId)
        {
            var data = await _repo.GetBySchoolAsync(hostelId);
            return _mapper.Map<IEnumerable<SchoolHolidayDTO>>(data);
        }
    }
}
