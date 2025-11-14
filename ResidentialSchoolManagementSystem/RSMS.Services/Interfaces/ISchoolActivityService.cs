using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface ISchoolActivityService
    {
        Task<Guid> CreateAsync(SchoolActivityDTO dto, Guid userId);
        Task<bool> UpdateAsync(Guid id, SchoolActivityDTO dto, Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<List<SchoolActivityDTO>> GetByHostelAsync(Guid hostelId);
        Task<SchoolActivityDTO?> GetByIdAsync(Guid id);
    }

}
