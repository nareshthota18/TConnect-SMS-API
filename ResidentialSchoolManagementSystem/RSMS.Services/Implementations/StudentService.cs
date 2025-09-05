using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly RSMSDbContext _context;

        public StudentService(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentByIdAsync(long id) =>
            await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel)
                .FirstOrDefaultAsync(s => s.StudentId == id);

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() =>
            await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel)
                .ToListAsync();

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
