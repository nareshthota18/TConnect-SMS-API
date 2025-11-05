using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDTO>> GetAllAsync(Guid RSHostelId);
        Task<StaffDTO?> GetByIdAsync(Guid id);
        Task<StaffDTO> AddAsync(StaffDTO staff, Guid RSHostelId);
        Task<StaffDTO> UpdateAsync(StaffDTO staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
