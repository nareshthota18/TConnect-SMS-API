using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            // access claims from the token
            var username = User.FindFirst("username")?.Value;
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("GetById/{id:Guid}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserDTO>> Create(UserDTO user)
        {
            var created = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("Update/{id:Guid}")]
        public async Task<ActionResult<UserDTO>> Update(Guid id, UserDTO user)
        {
            if (id != user.Id) return BadRequest("ID mismatch");
            var updated = await _userService.UpdateAsync(user);
            return Ok(updated);
        }

        [HttpDelete("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPut("ResetPassword")]
        public async Task<ActionResult> UpdatePassword(ResetPassword user)
        {
            var result = await _userService.UpdatePassword(user);
            return result ? Ok() : NotFound();
        }
    }

}
