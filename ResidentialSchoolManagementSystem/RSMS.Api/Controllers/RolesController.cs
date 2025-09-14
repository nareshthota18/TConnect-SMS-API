using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoleDTO>> GetById(Guid id)
        {
            var role = await _service.GetByIdAsync(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create(RoleDTO role)
        {
            var created = await _service.AddAsync(role);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoleDTO>> Update(Guid id, RoleDTO role)
        {
            if (id != role.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(role);
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
