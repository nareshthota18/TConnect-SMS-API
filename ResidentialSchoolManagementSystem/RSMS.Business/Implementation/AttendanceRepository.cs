using Microsoft.EntityFrameworkCore;
using RSMS.Business.Contracts;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Business.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly RSMSDbContext _context;

        public AttendanceRepository(RSMSDbContext context)
        {
            _context = context;
        }

        // Student Attendance
        public async Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync() =>
            await _context.StudentAttendance.ToListAsync();

        public async Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id) =>
            await _context.StudentAttendance.FindAsync(id);

        public async Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance att)
        {
            _context.StudentAttendance.Add(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance att)
        {
            _context.StudentAttendance.Update(att);
            await _context.SaveChangesAsync();
            return att;
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
        public async Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync() =>
            await _context.StaffAttendance.ToListAsync();

        public async Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id) =>
            await _context.StaffAttendance.FindAsync(id);

        public async Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance att)
        {
            _context.StaffAttendance.Add(att);
            await _context.SaveChangesAsync();
            return att;
        }

        public async Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance att)
        {
            _context.StaffAttendance.Update(att);
            await _context.SaveChangesAsync();
            return att;
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
