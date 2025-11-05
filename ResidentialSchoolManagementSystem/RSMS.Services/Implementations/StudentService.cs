using AutoMapper;
using RSMS.Common.DTO;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.LookupEntities;
using RSMS.Repositories.Contracts;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILookupRepository<Category, Guid> _lookupRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, ILookupRepository<Category, Guid> lookupRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _lookupRepository = lookupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync(Guid rSHostelId)
        {
            var students = await _studentRepository.GetAllAsync(rSHostelId);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }
        public async Task<IEnumerable<StudentDTO>> StudentsByGrade(Guid GradeId)
        {
            var students = await _studentRepository.GetStudentsByGrade(GradeId);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO?> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student == null ? null : _mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> AddStudentAsync(StudentDTO dto)
        {
            var student = _mapper.Map<Student>(dto);
            var created = await _studentRepository.AddAsync(student);
            return _mapper.Map<StudentDTO>(created);
        }

        public async Task<StudentDTO> UpdateStudentAsync(StudentDTO dto)
        {
            var categoryExists = await _lookupRepository.ExistsAsync(dto.CategoryId.Value);
            if (!categoryExists)
            {
                throw new InvalidOperationException("The specified CategoryId does not exist.");
            }

            var student = _mapper.Map<Student>(dto);
            var updated = await _studentRepository.UpdateAsync(student);
            return _mapper.Map<StudentDTO>(updated);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
