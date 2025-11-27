using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync(Guid rSHostelId);
        Task<StudentDTO?> GetStudentByIdAsync(Guid id);
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(StudentDTO student);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<IEnumerable<StudentDTO>> StudentsByGrade(Guid GradeId, Guid rSHostelId);
    }
}
