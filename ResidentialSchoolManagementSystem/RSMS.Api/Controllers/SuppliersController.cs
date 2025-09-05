using Microsoft.AspNetCore.Mvc;
using RSMS.Data.Models.LookupEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SuppliersController(ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Supplier>> GetById(Guid id)
        {
            var supplier = await _service.GetByIdAsync(id);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            var created = await _service.AddAsync(supplier);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Supplier>> Update(Guid id, Supplier supplier)
        {
            if (id != supplier.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(supplier);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
