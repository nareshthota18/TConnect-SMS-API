﻿using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        // Student attendance
        Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync();
        Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id);
        Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance att);
        Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance att);
        Task<bool> DeleteStudentAttendanceAsync(Guid id);

        // Staff attendance
        Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync();
        Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id);
        Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance att);
        Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance att);
        Task<bool> DeleteStaffAttendanceAsync(Guid id);
    }
}
