using RSMS.Common.Models;

namespace RSMS.Repositories.Contracts
{
    public interface IStaffRepository
    {
        Task<IEnumerable<StaffDTO>> GetAllAsync(Guid RSHostelId);
        Task<StaffDTO?> GetByIdAsync(Guid id);
        Task<StaffDTO> AddAsync(StaffDTO staff, Guid RSHostelId);
        Task<StaffDTO> UpdateAsync(StaffDTO staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
