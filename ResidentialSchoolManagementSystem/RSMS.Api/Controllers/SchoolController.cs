using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<HostelDTO>>> GetAll()
        {
            // access claims from the token
            var userId = User.FindFirst("userId")?.Value;
            var dts = await _schoolService.GetAllAsync(Guid.Parse(userId));
            return Ok(dts);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] HostelDTO dt)
        {
            dt.Id = Guid.Parse(User.FindFirst("userId").Value);
            await _schoolService.AddAsync(dt);
            return Ok();
        }

        [HttpDelete("Delete/{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _schoolService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
