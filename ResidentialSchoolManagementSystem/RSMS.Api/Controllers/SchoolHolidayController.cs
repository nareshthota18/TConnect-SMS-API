using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;
using System.Security.Claims;

namespace RSMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolHolidayController : ControllerBase
    {
        private readonly ISchoolHolidayService _holidayService;

        public SchoolHolidayController(ISchoolHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        private Guid CurrentUserId =>
            Guid.Parse(User.FindFirstValue("userId"));

        [HttpGet("{hostelId:Guid}")]
        public async Task<IActionResult> GetBySchool(Guid hostelId)
        {
            var data = await _holidayService.GetBySchoolAsync(hostelId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolHolidayDTO dto)
        {
            await _holidayService.AddAsync(dto, CurrentUserId);
            return Ok(new { message = "Holiday added." });
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SchoolHolidayDTO dto)
        {
            dto.Id = id;
            await _holidayService.UpdateAsync(dto, CurrentUserId);
            return Ok(new { message = "Holiday updated." });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _holidayService.DeleteAsync(id);
            return res ? NoContent() : NotFound();
        }
    }
}
