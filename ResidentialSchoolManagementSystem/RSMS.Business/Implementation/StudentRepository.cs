using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentRepository(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<StudentDTO> AddStudentAsync(StudentDTO student)
        {
            var std = _mapper.Map<Student>(student);
            var newstd = await _studentService.AddStudentAsync(std);
            return _mapper.Map<StudentDTO>(newstd);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await _studentService.DeleteStudentAsync(id);
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO?> GetStudentByIdAsync(Guid id)
        {
            var std = await _studentService.GetStudentByIdAsync(id);
            return _mapper.Map<StudentDTO>(std);
        }

        public async Task<StudentDTO> UpdateStudentAsync(StudentDTO student)
        {
            var std = _mapper.Map<Student>(student);
            var newstd = await _studentService.UpdateStudentAsync(std);
            return _mapper.Map<StudentDTO>(newstd);
        }
    }
}
