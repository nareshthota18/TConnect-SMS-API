using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByIdAsync(long id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> AddStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(long id);
    }
}
