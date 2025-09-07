using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IAttendanceRepository
    {
        Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(Guid id);
        Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync();
        Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO attendance);
        Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO attendance);
        Task<bool> DeleteStudentAttendanceAsync(Guid id);

        Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(Guid id);
        Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync();
        Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO attendance);
        Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO attendance);
        Task<bool> DeleteStaffAttendanceAsync(Guid id);
    }
}
