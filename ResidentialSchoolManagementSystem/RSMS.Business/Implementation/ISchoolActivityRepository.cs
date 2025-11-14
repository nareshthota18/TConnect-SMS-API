using RSMS.Data.Models.Others;

namespace RSMS.Repositories.Implementation
{
    public interface ISchoolActivityRepository
    {
        Task AddAsync(SchoolActivity entity);
        Task UpdateAsync(SchoolActivity entity);
        Task<SchoolActivity?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<List<SchoolActivity>> GetByHostelAsync(Guid hostelId);
    }

}
