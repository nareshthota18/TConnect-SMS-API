using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _service;

        public AttendanceController(IAttendanceRepository service)
        {
            _service = service;
        }

        // Student Attendance
        [HttpGet("students")]
        public async Task<ActionResult<IEnumerable<StudentAttendanceDTO>>> GetAllStudentAttendance()
            => Ok(await _service.GetAllStudentAttendanceAsync());

        [HttpGet("students/{id:long}")]
        public async Task<ActionResult<StudentAttendanceDTO>> GetStudentAttendance(Guid id)
        {
            var att = await _service.GetStudentAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("students")]
        public async Task<ActionResult<StudentAttendanceDTO>> CreateStudentAttendance(StudentAttendanceDTO att)
        {
            var created = await _service.AddStudentAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStudentAttendance), new { id = created.AttendanceId }, created);
        }

        [HttpPut("students/{id:long}")]
        public async Task<ActionResult<StudentAttendanceDTO>> UpdateStudentAttendance(Guid id, StudentAttendanceDTO att)
        {
            if (id != att.AttendanceId) return BadRequest("ID mismatch");
            var updated = await _service.UpdateStudentAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("students/{id:long}")]
        public async Task<IActionResult> DeleteStudentAttendance(Guid id)
        {
            var result = await _service.DeleteStudentAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }

        // Staff Attendance
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<StaffAttendanceDTO>>> GetAllStaffAttendance()
            => Ok(await _service.GetAllStaffAttendanceAsync());

        [HttpGet("staff/{id:long}")]
        public async Task<ActionResult<StaffAttendanceDTO>> GetStaffAttendance(Guid id)
        {
            var att = await _service.GetStaffAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("staff")]
        public async Task<ActionResult<StaffAttendanceDTO>> CreateStaffAttendance(StaffAttendanceDTO att)
        {
            var created = await _service.AddStaffAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStaffAttendance), new { id = created.StaffAttendanceId }, created);
        }

        [HttpPut("staff/{id:long}")]
        public async Task<ActionResult<StaffAttendanceDTO>> UpdateStaffAttendance(Guid id, StaffAttendanceDTO att)
        {
            if (id != att.StaffAttendanceId) return BadRequest("ID mismatch");
            var updated = await _service.UpdateStaffAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("staff/{id:long}")]
        public async Task<IActionResult> DeleteStaffAttendance(Guid id)
        {
            var result = await _service.DeleteStaffAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
