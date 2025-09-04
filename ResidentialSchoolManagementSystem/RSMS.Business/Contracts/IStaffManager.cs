using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IStaffManager
    {
        Task<StaffDTO?> GetByIdAsync(long id);
        Task<IEnumerable<StaffDTO>> GetAllAsync();
        Task<StaffDTO> AddAsync(StaffDTO staff);
        Task<StaffDTO> UpdateAsync(StaffDTO staff);
        Task<bool> DeleteAsync(long id);
    }
}
