using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMS.Common.Models;

namespace RSMS.Business.Contracts
{
    public interface IAttendanceManager
    {
        Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(long id);
        Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync();
        Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO attendance);
        Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO attendance);
        Task<bool> DeleteStudentAttendanceAsync(long id);

        Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(long id);
        Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync();
        Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO attendance);
        Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO attendance);
        Task<bool> DeleteStaffAttendanceAsync(long id);
    }
}
