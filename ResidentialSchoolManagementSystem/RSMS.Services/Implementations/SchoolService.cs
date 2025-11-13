using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.LookupEntities;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(HostelDTO dto)
        {
            var hs = _mapper.Map<RSHostel>(dto);
            hs.CreatedBy = dto.Id;
            hs.UpdatedBy = dto.Id;
            await _schoolRepository.AddAsync(hs);
        }

        public async Task<IEnumerable<HostelDTO>> GetAllAsync(Guid userId)
        {
            var users = await _schoolRepository.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<HostelDTO>>(users);
        }
        public async Task<IEnumerable<HostelDTO>> GetAllAsync()
        {
            var users = await _schoolRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HostelDTO>>(users);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _schoolRepository.DeleteAsync(id);
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _schoolRepository.ExistsByNameAsync(name);
        }
    }
}
