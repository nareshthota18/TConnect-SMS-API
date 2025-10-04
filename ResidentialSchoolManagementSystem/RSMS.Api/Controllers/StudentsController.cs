using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<StudentDTO>> GetById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Create(StudentDTO student)
        {
            var created = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<StudentDTO>> Update(Guid id, StudentDTO student)
        {
            if (id != student.Id) return BadRequest("ID mismatch");

            var updated = await _studentService.UpdateStudentAsync(student);
            return Ok(updated);
        }

        [HttpDelete("Delete/{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            return result ? NoContent() : NotFound();
        }

        
    }
}
