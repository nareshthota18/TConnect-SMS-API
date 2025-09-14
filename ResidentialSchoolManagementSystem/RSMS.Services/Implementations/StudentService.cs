using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
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
