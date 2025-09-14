using RSMS.Common.Models;

namespace RSMS.Business.Contracts
{
    public interface IStaffRepository
    {
        Task<IEnumerable<StaffDTO>> GetAllAsync();
        Task<StaffDTO?> GetByIdAsync(Guid id);
        Task<StaffDTO> AddAsync(StaffDTO staff);
        Task<StaffDTO> UpdateAsync(StaffDTO staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
