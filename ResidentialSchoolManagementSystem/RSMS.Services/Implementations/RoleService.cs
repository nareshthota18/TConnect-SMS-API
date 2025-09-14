using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(entities);
        }

        public async Task<RoleDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<RoleDTO>(entity);
        }

        public async Task<RoleDTO> AddAsync(RoleDTO role)
        {
            var entity = _mapper.Map<Role>(role);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<RoleDTO>(created);
        }

        public async Task<RoleDTO> UpdateAsync(RoleDTO role)
        {
            var entity = _mapper.Map<Role>(role);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<RoleDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
