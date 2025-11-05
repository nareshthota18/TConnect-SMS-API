using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSMS.Common.DTO;
using RSMS.Data.Models.LookupEntities;
using RSMS.Services.Contracts;

namespace RSMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService<Category, Guid> _categoryService;
        private readonly ILookupService<Department, Guid> _departmentService;
        private readonly ILookupService<Designation, Guid> _designationService;
        private readonly ILookupService<Grade, Guid> _gradeService;

        public LookupController(
            ILookupService<Category, Guid> categoryService,
            ILookupService<Department, Guid> departmentService,
            ILookupService<Designation, Guid> designationService,
            ILookupService<Grade, Guid> gradeService)
        {
            _categoryService = categoryService;
            _departmentService = departmentService;
            _designationService = designationService;
            _gradeService = gradeService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<LookupDTO>>> GetCategories()
        {
            var result = await _categoryService.GetLookupAsync(c => new LookupDTO
            {
                Key = c.Id,
                Value = c.Name
            });
            return Ok(result);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<List<LookupDTO>>> GetDepartments()
        {
            var result = await _departmentService.GetLookupAsync(d => new LookupDTO
            {
                Key = d.Id,
                Value = d.Name
            });
            return Ok(result);
        }

        [HttpGet("designations")]
        public async Task<ActionResult<List<LookupDTO>>> GetDesignations()
        {
            var result = await _designationService.GetLookupAsync(d => new LookupDTO
            {
                Key = d.Id,
                Value = d.Name
            });
            return Ok(result);
        }

        [HttpGet("grades")]
        public async Task<ActionResult<List<LookupDTO>>> GetGrades()
        {
            var result = await _gradeService.GetLookupAsync(g => new LookupDTO
            {
                Key = g.Id,
                Value = g.Name
            });
            return Ok(result);
        }
    }

}
