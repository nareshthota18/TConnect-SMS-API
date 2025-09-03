using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<StudentAttendance?> GetStudentAttendanceByIdAsync(long id);
        Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync();
        Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance attendance);
        Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance attendance);
        Task<bool> DeleteStudentAttendanceAsync(long id);

        Task<StaffAttendance?> GetStaffAttendanceByIdAsync(long id);
        Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync();
        Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance attendance);
        Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance attendance);
        Task<bool> DeleteStaffAttendanceAsync(long id);
    }
}
