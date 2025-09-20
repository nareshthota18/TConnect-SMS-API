using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;

namespace RSMS.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly RSMSDbContext _context;

        public StudentRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == student.Id);
            if (existingStudent == null) throw new KeyNotFoundException();

            _context.Entry(existingStudent).CurrentValues.SetValues(student);

            await _context.SaveChangesAsync();
            return existingStudent;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
