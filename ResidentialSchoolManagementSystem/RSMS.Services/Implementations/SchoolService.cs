using AutoMapper;
using RSMS.Common.Models;
using RSMS.Common;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Repositories.Contracts;
using RSMS.Repositories.Implementation;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMS.Data.Models.LookupEntities;

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

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _schoolRepository.DeleteAsync(id);
        }
    }
}
