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
        private readonly ILookupService<AttendanceType, Guid> _attendanceTypeService;

        public LookupController(
            ILookupService<Category, Guid> categoryService,
            ILookupService<Department, Guid> departmentService,
            ILookupService<Designation, Guid> designationService,
            ILookupService<Grade, Guid> gradeService,
            ILookupService<AttendanceType, Guid> attendanceTypeService)
        {
            _categoryService = categoryService;
            _departmentService = departmentService;
            _designationService = designationService;
            _gradeService = gradeService;
            _attendanceTypeService = attendanceTypeService;
        }

        #region GET

        [HttpGet("categories")]
        public async Task<ActionResult<List<LookupDTO>>> GetCategories()
        {
            var result = await _categoryService.GetLookupAsync(c => new LookupDTO { Key = c.Id, Value = c.Name });
            return Ok(result);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<List<LookupDTO>>> GetDepartments()
        {
            var result = await _departmentService.GetLookupAsync(d => new LookupDTO { Key = d.Id, Value = d.Name });
            return Ok(result);
        }

        [HttpGet("designations")]
        public async Task<ActionResult<List<LookupDTO>>> GetDesignations()
        {
            var result = await _designationService.GetLookupAsync(d => new LookupDTO { Key = d.Id, Value = d.Name });
            return Ok(result);
        }

        [HttpGet("grades")]
        public async Task<ActionResult<List<LookupDTO>>> GetGrades()
        {
            var result = await _gradeService.GetLookupAsync(g => new LookupDTO { Key = g.Id, Value = g.Name });
            return Ok(result);
        }

        [HttpGet("attendancetypes")]
        public async Task<ActionResult<List<LookupDTO>>> GetAttendanceTypes()
        {
            var result = await _attendanceTypeService.GetLookupAsync(a => new LookupDTO { Key = a.Id, Value = a.Name });
            return Ok(result);
        }

        #endregion

        #region POST

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] LookupDTO dto)
            => await CreateLookupAsync(dto, _categoryService, "Category");

        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment([FromBody] LookupDTO dto)
            => await CreateLookupAsync(dto, _departmentService, "Department");

        [HttpPost("designations")]
        public async Task<IActionResult> CreateDesignation([FromBody] LookupDTO dto)
            => await CreateLookupAsync(dto, _designationService, "Designation");

        [HttpPost("grades")]
        public async Task<IActionResult> CreateGrade([FromBody] LookupDTO dto)
            => await CreateLookupAsync(dto, _gradeService, "Grade");

        [HttpPost("attendancetypes")]
        public async Task<IActionResult> CreateAttendanceType([FromBody] LookupDTO dto)
            => await CreateLookupAsync(dto, _attendanceTypeService, "Attendance type");

        #endregion

        #region PUT

        [HttpPut("categories/{id:Guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] LookupDTO dto)
            => await UpdateLookupAsync(id, dto, _categoryService, "Category");

        [HttpPut("departments/{id:Guid}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] LookupDTO dto)
            => await UpdateLookupAsync(id, dto, _departmentService, "Department");

        [HttpPut("designations/{id:Guid}")]
        public async Task<IActionResult> UpdateDesignation(Guid id, [FromBody] LookupDTO dto)
            => await UpdateLookupAsync(id, dto, _designationService, "Designation");

        [HttpPut("grades/{id:Guid}")]
        public async Task<IActionResult> UpdateGrade(Guid id, [FromBody] LookupDTO dto)
            => await UpdateLookupAsync(id, dto, _gradeService, "Grade");

        [HttpPut("attendancetypes/{id:Guid}")]
        public async Task<IActionResult> UpdateAttendanceType(Guid id, [FromBody] LookupDTO dto)
            => await UpdateLookupAsync(id, dto, _attendanceTypeService, "Attendance type");

        #endregion

        #region Helpers

        private async Task<IActionResult> CreateLookupAsync<TEntity>(
            LookupDTO dto,
            ILookupService<TEntity, Guid> service,
            string typeName) where TEntity : class, new()
        {
            if (string.IsNullOrWhiteSpace(dto.Value))
                return BadRequest(new { message = "Name is required." });

            var exists = await service.ExistsByNameAsync(dto.Value);
            if (exists)
                return Conflict(new { message = $"{typeName} '{dto.Value}' already exists." });

            var entity = new TEntity();
            entity.GetType().GetProperty("Id")?.SetValue(entity, Guid.NewGuid());
            entity.GetType().GetProperty("Name")?.SetValue(entity, dto.Value);
            entity.GetType().GetProperty("IsActive")?.SetValue(entity, true);

            await service.AddAsync(entity);
            return Ok(new { message = $"{typeName} added successfully." });
        }

        private async Task<IActionResult> UpdateLookupAsync<TEntity>(
            Guid id,
            LookupDTO dto,
            ILookupService<TEntity, Guid> service,
            string typeName) where TEntity : class
        {
            var existing = await service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"{typeName} not found." });

            existing.GetType().GetProperty("Name")?.SetValue(existing, dto.Value);
            await service.UpdateAsync(existing);
            return Ok(new { message = $"{typeName} updated successfully." });
        }

        #endregion
    }
}
