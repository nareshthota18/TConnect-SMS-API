using Microsoft.AspNetCore.Mvc;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAll()
        {
            var assets = await _service.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AssetDTO>> GetById(Guid id)
        {
            var asset = await _service.GetByIdAsync(id);
            return asset == null ? NotFound() : Ok(asset);
        }

        [HttpPost]
        public async Task<ActionResult<AssetDTO>> Create(AssetDTO asset)
        {
            var created = await _service.AddAsync(asset);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<AssetDTO>> Update(Guid id, AssetDTO asset)
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
