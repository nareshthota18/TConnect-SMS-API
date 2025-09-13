using AutoMapper;
using Azure.Core;
using RSMS.Business.Contracts;
using RSMS.Common;
using RSMS.Common.Models;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserService _userService;
        public UserRepository(IUserService userService)
        {
            _userService = userService;

        }
        public Task<UserDTO> AddAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidUser(string userName, string password)
        {
            bool isValid = false;
            var user = await _userService.Getuser(userName);
           // UserDTO userDt =  _mapper.Map<UserDTO>(user);
            if (user != null)
            {
                isValid = GeneratePasswordHash.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            }
            return isValid;
        }
    }
}
