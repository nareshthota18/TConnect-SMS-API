using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetsController(IAssetService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AssetIssueDTO>>> GetAll()
        {
            var assets = await _service.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AssetIssueDTO>> GetById(Guid id)
        {
            var asset = await _service.GetByIdAsync(id);
            return asset == null ? NotFound() : Ok(asset);
        }

        [HttpPost]
        public async Task<ActionResult<AssetIssueDTO>> Create(AssetIssueDTO asset)
        {
            var created = await _service.AddAsync(asset);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<AssetIssueDTO>> Update(Guid id, AssetIssueDTO asset)
        {
            if (id != asset.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(asset);
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
