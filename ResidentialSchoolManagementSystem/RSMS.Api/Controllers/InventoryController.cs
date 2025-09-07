using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _service;

        public InventoryController(IInventoryRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAll()
            => Ok(await _service.GetAllItemsAsync());

        [HttpGet("{id:int}")]
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Item>> Update(Guid id, ItemDTO item)
        {
            if (id != item.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateItemAsync(item);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteItemAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
