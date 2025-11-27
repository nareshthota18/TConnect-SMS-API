using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Repositories.Contracts;

namespace RSMS.Repositories.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly RSMSDbContext _context;

        public AttendanceRepository(RSMSDbContext context)
        {
            _context = context;
        }

        // ---------------- Student Attendance ----------------

        public async Task<StudentAttendance> AddStudentAttendanceAsync(StudentAttendance entity)
        {
            _context.StudentAttendance.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<StudentAttendance>> CreateStudentAttendanceList(List<StudentAttendance> entities)
        {
            _context.StudentAttendance.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<StudentAttendance?> GetStudentAttendanceByIdAsync(Guid id)
        {
            return await _context.StudentAttendance
                .Include(a => a.Student)        // Include navigation
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<StudentAttendance>> GetAllStudentAttendanceAsync(Guid RSHostelId)
        {
            return await _context.StudentAttendance
                .Include(a => a.Student)
                .Where(a => a.Student.RSHostelId == RSHostelId)
                .ToListAsync();
        }

        public async Task<StudentAttendance> UpdateStudentAttendanceAsync(StudentAttendance entity)
        {
            var existing = await _context.StudentAttendance
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.Id == entity.Id);

            if (existing == null)
                throw new KeyNotFoundException("Student attendance record not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteStudentAttendanceAsync(Guid id)
        {
            var entity = await _context.StudentAttendance.FindAsync(id);
            if (entity == null) return false;

            _context.StudentAttendance.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------- Staff Attendance ----------------

        public async Task<StaffAttendance> AddStaffAttendanceAsync(StaffAttendance entity)
        {
            _context.StaffAttendance.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<StaffAttendance>> CreateStaffAttendanceList(List<StaffAttendance> entities)
        {
            _context.StaffAttendance.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<StaffAttendance?> GetStaffAttendanceByIdAsync(Guid id)
        {
            return await _context.StaffAttendance.FindAsync(id);
        }

        public async Task<IEnumerable<StaffAttendance>> GetAllStaffAttendanceAsync(Guid RSHostelId)
        {
            return await _context.StaffAttendance
                .Include(a => a.Staff) 
                .Where(a => a.Staff.RSHostelId == RSHostelId)
                .ToListAsync();
        }

        public async Task<StaffAttendance> UpdateStaffAttendanceAsync(StaffAttendance entity)
        {
            var existing = await _context.StaffAttendance.FindAsync(entity.Id);
            if (existing == null)
                throw new KeyNotFoundException("Staff attendance record not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteStaffAttendanceAsync(Guid id)
        {
            var entity = await _context.StaffAttendance.FindAsync(id);
            if (entity == null) return false;

            _context.StaffAttendance.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
