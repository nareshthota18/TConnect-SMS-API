using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;

        public StaffService(IStaffRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<StaffDTO?> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<StaffDTO> AddAsync(StaffDTO staff) =>
            await _repository.AddAsync(staff);

        public async Task<StaffDTO> UpdateAsync(StaffDTO staff) =>
            await _repository.UpdateAsync(staff);

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
