using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;
using RSMS.Common.DTO;

namespace RSMS.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly RSMSDbContext _context;

        public StudentRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync(Guid rSHostelId)
        {
            return await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel)
                .Where(s => s.RSHostelId == rSHostelId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByGrade(Guid GradeId, Guid rSHostelId)
        {
            return await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.RSHostel).Where(s => s.GradeId == GradeId && s.RSHostelId == rSHostelId)
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
        public async Task<List<StudentAssessment>> CreateStudentAssessmentList(List<StudentAssessment> dto)
        {
            if (dto == null || !dto.Any()) return new List<StudentAssessment>();

            // 1. Get unique identifiers from the incoming list to filter the DB
            var studentIds = dto.Select(x => x.StudentId).Distinct().ToList();
            var date = dto.First().AssessmentDate; // Assuming the list is for the same date

            // 2. Fetch existing records for these students on this specific date
            var existingRecords = await _context.StudentAssessment
                .Where(x => studentIds.Contains(x.StudentId) && x.AssessmentDate == date)
                .ToListAsync();

            foreach (var incoming in dto)
            {
                incoming.Student = null!;
                incoming.Department = null!;
                // 3. Check if the record already exists
                var existing = existingRecords.FirstOrDefault(x =>
                    x.StudentId == incoming.StudentId &&
                    x.DepartmentId == incoming.DepartmentId &&
                    x.AssessmentTypeId == incoming.AssessmentTypeId);

                if (existing != null)
                {
                    // UPDATE: Map new values to the existing tracked entity
                    existing.ActualScore = incoming.ActualScore;
                    existing.MaxScore = incoming.MaxScore;
                    existing.Status = incoming.Status;
                    existing.UpdatedAt = DateTime.UtcNow;
                    existing.UpdatedBy = incoming.UpdatedBy;

                    _context.StudentAssessment.Update(existing);
                }
                else
                {
                    // INSERT: New record
                    incoming.Id = Guid.NewGuid();
                    incoming.CreatedAt = DateTime.UtcNow;
                    await _context.StudentAssessment.AddAsync(incoming);
                }
            }

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<List<StudentAssessment>> GetStudentAssessments(Guid rSHostelId)
        {
            var query = await _context.StudentAssessment
               .Include(s => s.Student)
               .Include(s => s.Department)
              .Where(s => s.Student.RSHostelId == rSHostelId).ToListAsync();

            return query;
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

            var students = await _context.StudentAttendance.Where(u => u.StudentId == id).ToListAsync();
            if (students.Any())
            {
                _context.StudentAttendance.RemoveRange(students);
                await _context.SaveChangesAsync();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
