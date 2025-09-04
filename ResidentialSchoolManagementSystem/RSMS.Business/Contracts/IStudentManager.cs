using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IStudentManager
    {
        Task<StudentDTO?> GetStudentByIdAsync(long id);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(StudentDTO student);
        Task<bool> DeleteStudentAsync(long id);
    }
}
