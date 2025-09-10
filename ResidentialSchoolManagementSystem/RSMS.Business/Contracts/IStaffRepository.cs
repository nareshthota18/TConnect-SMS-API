using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IStaffRepository
    {
        Task<StaffDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<StaffDTO>> GetAllAsync();
        Task<StaffDTO> AddAsync(StaffDTO staff);
        Task<StaffDTO> UpdateAsync(StaffDTO staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
