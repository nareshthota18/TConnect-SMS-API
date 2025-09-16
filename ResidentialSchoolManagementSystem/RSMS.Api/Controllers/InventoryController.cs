using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAll()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ItemDTO>> GetById(Guid id)
        {
            var item = await _service.GetItemByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> Create(ItemDTO item)
        {
            var created = await _service.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ItemDTO>> Update(Guid id, ItemDTO item)
        {
            if (id != item.Id) return BadRequest("ID mismatch");
            var updated = await _service.UpdateItemAsync(item);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteItemAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
