using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IStaffService
    {
        Task<Staff?> GetByIdAsync(Guid id);
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> AddAsync(Staff staff);
        Task<Staff> UpdateAsync(Staff staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
