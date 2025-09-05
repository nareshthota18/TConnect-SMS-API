using Microsoft.AspNetCore.Mvc;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Staff>> GetById(Guid id)
        {
            var staff = await _service.GetByIdAsync(id);
            return staff == null ? NotFound() : Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> Create(Staff staff)
        {
            var created = await _service.AddAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<Staff>> Update(Guid id, Staff staff)
        {
            if (id != staff.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(staff);
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
