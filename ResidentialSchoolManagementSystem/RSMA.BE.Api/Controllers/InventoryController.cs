using Microsoft.AspNetCore.Mvc;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
            => Ok(await _service.GetAllItemsAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Create(Item item)
        {
            var created = await _service.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.ItemId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Item>> Update(int id, Item item)
        {
            if (id != item.ItemId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateItemAsync(item);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteItemAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
