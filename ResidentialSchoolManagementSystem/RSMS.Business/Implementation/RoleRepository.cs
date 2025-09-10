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
    public class RoleRepository : IRoleRepository
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleRepository(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        public Task<RoleDTO> AddAsync(RoleDTO role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoleDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDTO> UpdateAsync(RoleDTO role)
        {
            throw new NotImplementedException();
        }
    }
}
