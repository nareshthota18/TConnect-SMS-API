using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Filters;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System.Security.Claims;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IUserService _userService;
        public SchoolController(ISchoolService schoolService, IUserService userService)
        {
            _schoolService = schoolService;
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUserSchools()
        {
            // Get userId from JWT claims
            var userIdClaim = User.FindFirstValue("userId");
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim);

            // Fetch user's schools from the service
            var userSchools = await _userService.GetUserHostelsAsync(userId);

            if (userSchools == null || !userSchools.Any())
                return NotFound(new { message = "User has no assigned schools" });

            // Map to simple response
            var result = userSchools.Select(s => new
            {
                schoolId = s.RSHostelId,
                schoolName = s.RSHostel?.Name,
                roleId = s.RoleId,
                roleName = s.Role?.Name,
                isPrimary = s.IsPrimary
            });

            return Ok(result);
        }


        [HttpPost("Create")]
        [HostelAccess]
        public async Task<ActionResult> Create([FromBody] HostelDTO dt)
        {
            dt.Id = Guid.Parse(User.FindFirst("userId").Value);
            await _schoolService.AddAsync(dt);
            return Ok();
        }

        [HttpDelete("Delete/{id:Guid}")]
        [HostelAccess]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _schoolService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

    }
}
