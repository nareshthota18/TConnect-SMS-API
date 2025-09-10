using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsController(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        [HttpPost("GetAllAttendanceTimeRange")]
        public async Task<ActionResult<ReportRequestDTO>> GetAllAttendanceTimeRange(ReportRequestDTO att)
        {
            var result = await _reportsRepository.GetAllAttendanceTimeRange(att);
            return Ok(result);
        }
    }
}
