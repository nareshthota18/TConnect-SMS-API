using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Api.Extentions;
using RSMS.Api.Filters;
using RSMS.Common.DTO;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [HostelAccess]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            var result = await _inventoryService.GetAllBySchoolAsync(rSHostelId);
            return Ok(result);
        }

        [HttpGet("GetAGroceryAsync")]
        public async Task<IActionResult> GetAGroceryAsync()
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            var result = await _inventoryService.GetAGroceryAsync(rSHostelId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InventoryDTO dto)
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            dto.RSHostelId = rSHostelId; // ensure consistency even if not sent in body

            var result = await _inventoryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InventoryDTO dto)
        {
            var rSHostelId = HttpContext.GetRSHostelId();
            dto.RSHostelId = rSHostelId; // enforce scope check

            var updated = await _inventoryService.UpdateAsync(dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }
    }
}
