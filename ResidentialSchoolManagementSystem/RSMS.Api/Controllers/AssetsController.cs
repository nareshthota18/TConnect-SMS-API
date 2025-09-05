using Microsoft.AspNetCore.Mvc;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetsController(IAssetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetIssue>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AssetIssue>> GetById(Guid id)
        {
            var issue = await _service.GetByIdAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<ActionResult<AssetIssue>> Create(AssetIssue issue)
        {
            var created = await _service.AddAsync(issue);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<AssetIssue>> Update(Guid id, AssetIssue issue)
        {
            if (id != issue.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(issue);
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
