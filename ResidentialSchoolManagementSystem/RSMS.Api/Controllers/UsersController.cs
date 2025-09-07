using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _service;

        public UsersController(IUserRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(UserDTO user)
        {
            var created = await _service.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<UserDTO>> Update(Guid id, UserDTO user)
        {
            if (id != user.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(user);
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
