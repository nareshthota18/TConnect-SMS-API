using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;
using System.Security.Claims;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsumptionController : ControllerBase
    {
        private readonly IConsumptionService _consumptionService;

        public ConsumptionController(IConsumptionService consumptionService)
        {
            _consumptionService = consumptionService;
        }

        private Guid CurrentUserId =>
           Guid.Parse(User.FindFirstValue("userId"));

        [HttpGet("{rsHostelId:Guid}")]
        public async Task<IActionResult> GetAllAsync(Guid rsHostelId)
        {
            var data = await _consumptionService.GetAllAsync(rsHostelId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsumptionConfigDTO dto)
        {
            await _consumptionService.AddAsync(dto, CurrentUserId);
            return Ok(new { message = "Consumption added." });
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ConsumptionConfigDTO dto)
        {
            dto.Id = id;
            await _consumptionService.UpdateAsync(dto, CurrentUserId);
            return Ok(new { message = "Consumption updated." });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _consumptionService.DeleteAsync(id);
            return res ? NoContent() : NotFound();
        }
    }
}
