using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolActivitiesController : ControllerBase
    {
        private readonly ISchoolActivityService _service;

        public SchoolActivitiesController(ISchoolActivityService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolActivityDTO dto)
        {
            var userId = Guid.Parse(User.FindFirst("userId")?.Value!);
            var id = await _service.CreateAsync(dto, userId);
            return Ok(new { message = "Created", id });
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SchoolActivityDTO dto)
        {
            var userId = Guid.Parse(User.FindFirst("userId")!.Value);
            var ok = await _service.UpdateAsync(id, dto, userId);
            return ok ? Ok(new { message = "Updated" }) : NotFound();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("hostel/{hostelId:Guid}")]
        public async Task<IActionResult> GetByHostel(Guid hostelId)
        {
            var data = await _service.GetByHostelAsync(hostelId);
            return Ok(data);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            return data == null ? NotFound() : Ok(data);
        }
    }

}
