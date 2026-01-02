using RSMS.Common.DTO;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync(Guid rSHostelId);
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Student>> GetStudentsByGrade(Guid GradeId, Guid rSHostelId);
        Task<List<StudentAssessment>> CreateStudentAssessmentList(List<StudentAssessment> dto);
        Task<List<StudentAssessment>> GetStudentAssessments(Guid rSHostelId);
    }
}
