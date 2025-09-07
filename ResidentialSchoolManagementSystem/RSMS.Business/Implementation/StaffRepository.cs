using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class StaffRepository : IStaffRepository
    {
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
