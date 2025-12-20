using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Extentions;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;
using System.Security.Claims;

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

            Guid rSHostelId = Guid.Empty;
            if (!HttpContext.isSuperAdmin())
                rSHostelId = HttpContext.GetRSHostelId();
            var users = await _userService.GetAllAsync(rSHostelId);
            return Ok(users);
        }

        [HttpGet("GetById/{id:Guid}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO user)
        {
            var existingUser = await _userService.GetByuserAsync(user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists!");
            }
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
        public async Task<ActionResult> Delete(Guid id)
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

        [HttpGet("GetNotifications")]
        public async Task<ActionResult<IEnumerable<NotificationAuditDTO>>> GetUnreadNotificationsAsync()
        {
            // access claims from the token

            Guid rSHostelId = Guid.Empty;
            if (!HttpContext.isSuperAdmin())
                rSHostelId = HttpContext.GetRSHostelId();
            var notes = await _userService.GetUnreadNotificationsAsync(rSHostelId);
            return Ok(notes);
        }

        [HttpPut("UpdateNotifications")]
        public async Task<IActionResult> ReadNotificationsList(List<NotificationAuditDTO> att)
        {
            var updated = await _userService.ReadNotificationsList(att);
            return Ok(updated);
        }
    }

}
