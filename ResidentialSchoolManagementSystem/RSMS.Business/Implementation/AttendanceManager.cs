using RSMS.Business.Contracts;
using RSMS.Common.Models;


namespace RSMS.Business.Implementation
{
    public class AttendanceManager : IAttendanceManager
    {
        public Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStaffAttendanceAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStudentAttendanceAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }
    }
}
