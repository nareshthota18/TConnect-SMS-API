using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface ISchoolHolidayService
    {
        Task AddAsync(SchoolHolidayDTO dto, Guid userId);
        Task UpdateAsync(SchoolHolidayDTO dto, Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<SchoolHolidayDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<SchoolHolidayDTO>> GetBySchoolAsync(Guid hostelId);
    }
}
