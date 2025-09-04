using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class StaffManager : IStaffManager
    {
        public Task<StaffDTO> AddAsync(StaffDTO staff)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StaffDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO> UpdateAsync(StaffDTO staff)
        {
            throw new NotImplementedException();
        }
    }
}
