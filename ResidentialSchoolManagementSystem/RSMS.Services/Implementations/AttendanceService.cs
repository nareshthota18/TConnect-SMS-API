using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly RSMSDbContext _context;

        public AttendanceService(RSMSDbContext context)
        {
            _context = context;
        }

        // Student Attendance
        public async Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id) =>
            await _context.StudentAttendance.FindAsync(id);

        public async Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync() =>
            await _context.StudentAttendance.ToListAsync();

        public async Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance attendance)
        {
            _context.StudentAttendance.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance attendance)
        {
            _context.StudentAttendance.Update(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<bool> DeleteStudentAttendanceAsync(Guid id)
        {
            var att = await _context.StudentAttendance.FindAsync(id);
            if (att == null) return false;

            _context.StudentAttendance.Remove(att);
            await _context.SaveChangesAsync();
            return true;
        }

        // Staff Attendance
        public async Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id) =>
            await _context.StaffAttendance.FindAsync(id);

        public async Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync() =>
            await _context.StaffAttendance.ToListAsync();

        public async Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance attendance)
        {
            _context.StaffAttendance.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance attendance)
        {
            _context.StaffAttendance.Update(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<bool> DeleteStaffAttendanceAsync(Guid id)
        {
            var att = await _context.StaffAttendance.FindAsync(id);
            if (att == null) return false;

            _context.StaffAttendance.Remove(att);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
