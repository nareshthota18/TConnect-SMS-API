using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // ---------------- Student Attendance ----------------
        [HttpGet("students")]
        public async Task<ActionResult<IEnumerable<StudentAttendanceDTO>>> GetAllStudentAttendance()
            => Ok(await _attendanceService.GetAllStudentAttendanceAsync());

        [HttpGet("students/{id:guid}")]
        public async Task<ActionResult<StudentAttendanceDTO>> GetStudentAttendance(Guid id)
        {
            var att = await _attendanceService.GetStudentAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("students")]
        public async Task<ActionResult<StudentAttendanceDTO>> CreateStudentAttendance(StudentAttendanceDTO att)
        {
            var created = await _attendanceService.AddStudentAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStudentAttendance), new { id = created.Id }, created);
        }

        [HttpPut("students/{id:guid}")]
        public async Task<ActionResult<StudentAttendanceDTO>> UpdateStudentAttendance(Guid id, StudentAttendanceDTO att)
        {
            if (id != att.Id) return BadRequest("ID mismatch");
            var updated = await _attendanceService.UpdateStudentAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("students/{id:guid}")]
        public async Task<IActionResult> DeleteStudentAttendance(Guid id)
        {
            var result = await _attendanceService.DeleteStudentAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }

        // ---------------- Staff Attendance ----------------
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<StaffAttendanceDTO>>> GetAllStaffAttendance()
            => Ok(await _attendanceService.GetAllStaffAttendanceAsync());

        [HttpGet("staff/{id:guid}")]
        public async Task<ActionResult<StaffAttendanceDTO>> GetStaffAttendance(Guid id)
        {
            var att = await _attendanceService.GetStaffAttendanceByIdAsync(id);
            return att == null ? NotFound() : Ok(att);
        }

        [HttpPost("staff")]
        public async Task<ActionResult<StaffAttendanceDTO>> CreateStaffAttendance(StaffAttendanceDTO att)
        {
            var created = await _attendanceService.AddStaffAttendanceAsync(att);
            return CreatedAtAction(nameof(GetStaffAttendance), new { id = created.StaffAttendanceId }, created);
        }

        [HttpPut("staff/{id:guid}")]
        public async Task<ActionResult<StaffAttendanceDTO>> UpdateStaffAttendance(Guid id, StaffAttendanceDTO att)
        {
            if (id != att.StaffAttendanceId) return BadRequest("ID mismatch");
            var updated = await _attendanceService.UpdateStaffAttendanceAsync(att);
            return Ok(updated);
        }

        [HttpDelete("staff/{id:guid}")]
        public async Task<IActionResult> DeleteStaffAttendance(Guid id)
        {
            var result = await _attendanceService.DeleteStaffAttendanceAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
