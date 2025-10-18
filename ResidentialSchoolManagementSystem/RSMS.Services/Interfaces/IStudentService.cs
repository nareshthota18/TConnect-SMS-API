using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO?> GetStudentByIdAsync(Guid id);
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(StudentDTO student);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<IEnumerable<StudentDTO>> StudentsByGrade(Guid GradeId);
    }
}
