using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class RoleManager : IRoleManager
    {
        public Task<RoleDTO> AddAsync(RoleDTO role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoleDTO?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDTO> UpdateAsync(RoleDTO role)
        {
            throw new NotImplementedException();
        }
    }
}
