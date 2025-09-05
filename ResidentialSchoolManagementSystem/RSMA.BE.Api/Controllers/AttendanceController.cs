using Microsoft.AspNetCore.Mvc;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        // Student Attendance
        [HttpGet("students")]
        public async Task<ActionResult<IEnumerable<StudentAttendance>>> GetAllStudentAttendance()
            => Ok(await _service.GetAllStudentAttendanceAsync());

        [HttpGet("students/{id:long}")]
        public async Task<ActionResult<StudentAttendance>> GetStudentAttendance(long id)
        {
            var att = await _service.GetStudentAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("students")]
        public async Task<ActionResult<StudentAttendance>> CreateStudentAttendance(StudentAttendance att)
        {
            var created = await _service.AddStudentAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStudentAttendance), new { id = created.AttendanceId }, created);
        }

        [HttpPut("students/{id:long}")]
        public async Task<ActionResult<StudentAttendance>> UpdateStudentAttendance(long id, StudentAttendance att)
        {
            if (id != att.AttendanceId) return BadRequest("ID mismatch");
            var updated = await _service.UpdateStudentAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("students/{id:long}")]
        public async Task<IActionResult> DeleteStudentAttendance(long id)
        {
            var result = await _service.DeleteStudentAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }

        // Staff Attendance
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<StaffAttendance>>> GetAllStaffAttendance()
            => Ok(await _service.GetAllStaffAttendanceAsync());

        [HttpGet("staff/{id:long}")]
        public async Task<ActionResult<StaffAttendance>> GetStaffAttendance(long id)
        {
            var att = await _service.GetStaffAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("staff")]
        public async Task<ActionResult<StaffAttendance>> CreateStaffAttendance(StaffAttendance att)
        {
            var created = await _service.AddStaffAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStaffAttendance), new { id = created.StaffAttendanceId }, created);
        }

        [HttpPut("staff/{id:long}")]
        public async Task<ActionResult<StaffAttendance>> UpdateStaffAttendance(long id, StaffAttendance att)
        {
            if (id != att.StaffAttendanceId) return BadRequest("ID mismatch");
            var updated = await _service.UpdateStaffAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("staff/{id:long}")]
        public async Task<IActionResult> DeleteStaffAttendance(long id)
        {
            var result = await _service.DeleteStaffAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
