using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Extentions;
using RSMS.Api.Filters;
using RSMS.Common.DTO;
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
            if (HttpContext.isSuperAdmin())
            {
               var schol = await _schoolService.GetAllAsync();
                // Map to simple response
                var list = schol.Select(s => new
                {
                    schoolId = s.Id,
                    schoolName = s.Name,
                });
                return Ok(list);
            }
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
        public async Task<ActionResult> Create([FromBody] HostelDTO dto)
        {
            if (await _schoolService.ExistsByNameAsync(dto.Name))
                return Conflict(new { message = $"A school with the name '{dto.Name}' already exists." });

            dto.Id = Guid.NewGuid();
            await _schoolService.AddAsync(dto);

            return Ok(new { message = "School created successfully.", dto.Id });
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
