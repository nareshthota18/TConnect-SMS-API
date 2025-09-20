using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _service;

        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [HttpPost("GetAllAttendanceTimeRange")]
        public async Task<ActionResult<IEnumerable<AttendanceReportDTO>>> GetAllAttendanceTimeRange(ReportRequestDTO request)
        {
            var result = await _service.GetAllAttendanceTimeRange(request);
            return Ok(result);
        }
    }
}
