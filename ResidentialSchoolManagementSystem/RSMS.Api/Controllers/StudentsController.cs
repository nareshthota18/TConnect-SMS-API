using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentManager _service;

        public StudentsController(IStudentManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAll()
            => Ok(await _service.GetAllStudentsAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<StudentDTO>> GetById(long id)
        {
            var student = await _service.GetStudentByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Create(StudentDTO student)
        {
            var created = await _service.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = created.StudentId }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<StudentDTO>> Update(long id, StudentDTO student)
        {
            if (id != student.StudentId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateStudentAsync(student);
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteStudentAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
