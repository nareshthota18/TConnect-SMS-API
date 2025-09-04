using Microsoft.AspNetCore.Mvc;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetManager _service;

        public AssetsController(IAssetManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AssetIssue>> GetById(long id)
        {
            var issue = await _service.GetByIdAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<ActionResult<AssetDTO>> Create(AssetDTO issue)
        {
            var created = await _service.AddAsync(issue);
            return CreatedAtAction(nameof(GetById), new { id = created.IssueId }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<AssetDTO>> Update(long id, AssetDTO issue)
        {
            if (id != issue.IssueId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(issue);
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
