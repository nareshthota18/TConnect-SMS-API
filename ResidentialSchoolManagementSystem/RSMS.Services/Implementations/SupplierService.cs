using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.LookupEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllAsync()
        {
            var suppliers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<SupplierDTO?> GetByIdAsync(Guid id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            return supplier == null ? null : _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> AddAsync(SupplierDTO dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            var created = await _repository.AddAsync(supplier);
            return _mapper.Map<SupplierDTO>(created);
        }

        public async Task<SupplierDTO> UpdateAsync(SupplierDTO dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            var updated = await _repository.UpdateAsync(supplier);
            return _mapper.Map<SupplierDTO>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
