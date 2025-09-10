using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IUserRepository
    {
        Task<UserDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> AddAsync(UserDTO user);
        Task<UserDTO> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
    }
}
