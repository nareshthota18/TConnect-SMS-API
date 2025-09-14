using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;
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
            var created = await _userRepository.AddAsync(user);
            return _mapper.Map<UserDTO>(created);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var updated = await _userRepository.UpdateAsync(user);
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
    }
}
