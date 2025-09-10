using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
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
        private readonly IUserService  _userService;
        private readonly IMapper _mapper;
        public UserRepository(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
             
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
    }
}
