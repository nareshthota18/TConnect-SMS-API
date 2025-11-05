using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("GetById/{id:guid}")]
        public async Task<ActionResult<RoleDTO>> GetById(Guid id)
        {
            var role = await _service.GetByIdAsync(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<RoleDTO>> Create(RoleDTO role)
        {
            var created = await _service.AddAsync(role);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("Update/{id:guid}")]
        public async Task<ActionResult<RoleDTO>> Update(Guid id, RoleDTO role)
        {
            if (id != role.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(role);
            return Ok(updated);
        }

        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
