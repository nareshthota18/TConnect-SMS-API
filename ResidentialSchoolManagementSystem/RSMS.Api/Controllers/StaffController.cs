using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffManager _service;

        public StaffController(IStaffManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<StaffDTO>> GetById(long id)
        {
            var staff = await _service.GetByIdAsync(id);
            return staff == null ? NotFound() : Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDTO>> Create(StaffDTO staff)
        {
            var created = await _service.AddAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = created.StaffId }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<StaffDTO>> Update(long id, StaffDTO staff)
        {
            if (id != staff.StaffId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(staff);
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
