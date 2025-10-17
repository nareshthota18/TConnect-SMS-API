using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSMS.Common;
using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            // Generate password hash and salt for a default password. we can generate random password and send it to user email.
            (byte[] hashBytes, byte[] saltBytes) = GeneratePasswordHash.GetPasswordHash("Test@2025");
            user.PasswordHash = hashBytes;
            user.PasswordSalt = saltBytes;
            var created = await _userRepository.AddAsync(user);
            if (created != null)
            {
                await _userRepository.AddAUserRolesync(new UserHostel() { UserId = created.Id, RoleId = dto.RoleId });
            }

            return _mapper.Map<UserDTO>(created);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var updated = await _userRepository.UpdateAsync(user);
            if (dto.RoleId != Guid.Empty)
                await _userRepository.UpdateUserRolesync(new UserHostel() { UserId = dto.Id, RoleId = dto.RoleId });
            return _mapper.Map<UserDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidUser(string userName, string password)
        {
            return await _userRepository.ValidUser(userName, password);
        }

        public async Task<bool> UpdatePassword(ResetPassword user)
        {
            (byte[] hashBytes, byte[] saltBytes) = GeneratePasswordHash.GetPasswordHash(user.ConfirmPassword);
            return await _userRepository.UpdatePassword(user, hashBytes, saltBytes);
        }

        public async Task<List<UserHostel?>> GetUserHostelsAsync(Guid userId)
        {
            return await _userRepository.GetUserHostelsAsync(userId);
        }

        public async Task<User> GetByuserAsync(string userName)
        {
            return await _userRepository.GetByuserAsync(userName);
        }
    }
}
