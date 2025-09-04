using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class UserManager : IUserManager
    {
        public Task<UserDTO> AddAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
