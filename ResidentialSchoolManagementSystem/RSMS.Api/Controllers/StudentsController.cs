using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Extentions;
using RSMS.Api.Filters;
using RSMS.Common.DTO;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [HostelAccess]
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
            var rSHostelId = HttpContext.GetRSHostelId();
            var students = await _studentService.GetAllStudentsAsync(rSHostelId);
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
            var rSHostelId = HttpContext.GetRSHostelId();
            if (rSHostelId != student.RSHostelId) return BadRequest("School mismatch");
            var created = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<ActionResult<StudentDTO>> Update(StudentDTO student)
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            if (rSHostelId != student.RSHostelId) return BadRequest("School mismatch");

            var updated = await _studentService.UpdateStudentAsync(student);
            return Ok(updated);
        }

        [HttpDelete("Delete/{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("StudentsByGrade/{id:Guid}")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> StudentsByGrade(Guid id)
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            var students = await _studentService.StudentsByGrade(id, rSHostelId);
            return Ok(students);
        }

        [HttpPost("StudentAssessmentList")]
        public async Task<IActionResult> CreateStudentAssessmentList(List<StudentAssessmentDTO> att)
        {
            var updated = await _studentService.CreateStudentAssessmentList(att);
            return Ok(updated);
        }


        [HttpGet("GetStudentAssessments")]
        public async Task<ActionResult<IEnumerable<StudentAssessmentDTO>>> GetStudentAssessments()
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            var students = await _studentService.GetStudentAssessments(rSHostelId);
            return Ok(students);
        }

    }
}
