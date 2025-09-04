using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IRoleManager
    {
        Task<RoleDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO> AddAsync(RoleDTO role);
        Task<RoleDTO> UpdateAsync(RoleDTO role);
        Task<bool> DeleteAsync(int id);
    }
}
