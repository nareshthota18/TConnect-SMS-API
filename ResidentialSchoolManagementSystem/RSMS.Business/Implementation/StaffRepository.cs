using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffRepository(IRoleRepository staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }
        public Task<StaffDTO> AddAsync(StaffDTO staff)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StaffDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO> UpdateAsync(StaffDTO staff)
        {
            throw new NotImplementedException();
        }
    }
}
