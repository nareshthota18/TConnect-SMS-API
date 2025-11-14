using RSMS.Data.Models.Others;

namespace RSMS.Repositories.Contracts
{
    public interface ISchoolHolidayRepository
    {
        Task AddAsync(SchoolHoliday entity);
        Task UpdateAsync(SchoolHoliday entity);
        Task<bool> DeleteAsync(Guid id);
        Task<SchoolHoliday?> GetByIdAsync(Guid id);
        Task<IEnumerable<SchoolHoliday>> GetBySchoolAsync(Guid hostelId);
    }
}
