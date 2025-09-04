using RSMS.Business.Contracts;
using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class StudentManager : IStudentManager
    {
        public Task<StudentDTO> AddStudentAsync(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStudentAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO?> GetStudentByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> UpdateStudentAsync(StudentDTO student)
        {
            throw new NotImplementedException();
        }
    }
}
