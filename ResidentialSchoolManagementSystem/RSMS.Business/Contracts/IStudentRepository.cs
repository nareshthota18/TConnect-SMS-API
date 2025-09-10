using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IStudentRepository
    {
        Task<StudentDTO?> GetStudentByIdAsync(Guid id);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(StudentDTO student);
        Task<bool> DeleteStudentAsync(Guid id);
    }
}
