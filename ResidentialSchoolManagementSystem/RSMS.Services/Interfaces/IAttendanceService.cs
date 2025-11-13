using RSMS.Common.DTO;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Services.Interfaces
{
    public interface IAttendanceService
    {
        // Student attendance
        Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync();
        Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(Guid id);
        Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO att);
        Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO att);
        Task<bool> DeleteStudentAttendanceAsync(Guid id);

        // Staff attendance
        Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync();
        Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(Guid id);
        Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO att);
        Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO att);
        Task<bool> DeleteStaffAttendanceAsync(Guid id);
        Task<List<StaffAttendanceDTO>> CreateStaffAttendanceList(List<StaffAttendanceDTO> att);
        Task<List<StudentAttendanceDTO>> CreateStudentAttendanceList(List<StudentAttendanceDTO> att);
    }
}
