using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Extentions;
using RSMS.Api.Filters;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [HostelAccess]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAll()
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            return Ok(await _service.GetAllAsync(rSHostelId));
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StaffDTO>> GetById(Guid id)
        {
            var staff = await _service.GetByIdAsync(id);
            return staff == null ? NotFound() : Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDTO>> Create(StaffDTO staff)
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            var created = await _service.AddAsync(staff, rSHostelId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<ActionResult<StaffDTO>> Update(StaffDTO staff)
        {
            var updated = await _service.UpdateAsync(staff);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
