using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;


namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
      private readonly ISupplierManager _service;

        public SuppliersController(ISupplierManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SupplierDTO>> GetById(int id)
        {
            var supplier = await _service.GetByIdAsync(id);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Create(SupplierDTO supplier)
        {
            var created = await _service.AddAsync(supplier);
            return CreatedAtAction(nameof(GetById), new { id = created.SupplierId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SupplierDTO>> Update(int id, SupplierDTO supplier)
        {
            if (id != supplier.SupplierId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(supplier);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
