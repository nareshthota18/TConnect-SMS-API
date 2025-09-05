using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id);
        Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync();
        Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance attendance);
        Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance attendance);
        Task<bool> DeleteStudentAttendanceAsync(Guid id);

        Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id);
        Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync();
        Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance attendance);
        Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance attendance);
        Task<bool> DeleteStaffAttendanceAsync(Guid id);
    }
}
