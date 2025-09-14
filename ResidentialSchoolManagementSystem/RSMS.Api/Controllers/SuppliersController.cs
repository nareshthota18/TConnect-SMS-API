using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
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
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAll()
        {
            var suppliers = await _service.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<SupplierDTO>> GetById(Guid id)
        {
            var supplier = await _service.GetByIdAsync(id);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Create(SupplierDTO supplier)
        {
            var created = await _service.AddAsync(supplier);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<SupplierDTO>> Update(Guid id, SupplierDTO supplier)
        {
            if (id != supplier.Id)
                return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(supplier);
            return Ok(updated);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
