using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IRoleRepository
    {
        Task<RoleDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO> AddAsync(RoleDTO role);
        Task<RoleDTO> UpdateAsync(RoleDTO role);
        Task<bool> DeleteAsync(Guid id);
    }
}
