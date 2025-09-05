using Microsoft.AspNetCore.Mvc;
using Moq;
using RSMS.Api.Controllers;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Tests.Controllers
{
    public class AttendanceControllerTests
    {
        private readonly Mock<IAttendanceService> _mockService;
        private readonly AttendanceController _controller;

        public AttendanceControllerTests()
        {
            _mockService = new Mock<IAttendanceService>();
            _controller = new AttendanceController(_mockService.Object);
        }

        #region Student Attendance

        [Fact]
        public async Task GetAllStudentAttendance_ReturnsOk_WithList()
        {
            _mockService.Setup(s => s.GetAllStudentAttendanceAsync())
                .ReturnsAsync(new List<StudentAttendance> { new StudentAttendance { AttendanceId = 1 } });

            var result = await _controller.GetAllStudentAttendance();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<StudentAttendance>>(okResult.Value);
            Assert.Single(list);
        }

        [Fact]
        public async Task GetStudentAttendance_ReturnsOk_WhenExists()
        {
            _mockService.Setup(s => s.GetStudentAttendanceByIdAsync(1))
                .ReturnsAsync(new StudentAttendance { AttendanceId = 1 });

            var result = await _controller.GetStudentAttendance(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var att = Assert.IsType<StudentAttendance>(okResult.Value);
            Assert.Equal(1, att.AttendanceId);
        }

        [Fact]
        public async Task GetStudentAttendance_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetStudentAttendanceByIdAsync(999)).ReturnsAsync((StudentAttendance?)null);

            var result = await _controller.GetStudentAttendance(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateStudentAttendance_ReturnsCreatedAtAction()
        {
            var att = new StudentAttendance { AttendanceId = 1 };
            _mockService.Setup(s => s.AddStudentAttendanceAsync(att)).ReturnsAsync(att);

            var result = await _controller.CreateStudentAttendance(att);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdAtt = Assert.IsType<StudentAttendance>(createdResult.Value);
            Assert.Equal(1, createdAtt.AttendanceId);
        }

        [Fact]
        public async Task UpdateStudentAttendance_ReturnsOk_WhenUpdated()
        {
            var att = new StudentAttendance { AttendanceId = 1 };
            _mockService.Setup(s => s.UpdateStudentAttendanceAsync(att)).ReturnsAsync(att);

            var result = await _controller.UpdateStudentAttendance(1, att);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updated = Assert.IsType<StudentAttendance>(okResult.Value);
            Assert.Equal(1, updated.AttendanceId);
        }

        [Fact]
        public async Task UpdateStudentAttendance_ReturnsBadRequest_WhenIdMismatch()
        {
            var att = new StudentAttendance { AttendanceId = 2 };

            var result = await _controller.UpdateStudentAttendance(1, att);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("ID mismatch", badRequest.Value);
        }

        [Fact]
        public async Task DeleteStudentAttendance_ReturnsNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteStudentAttendanceAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteStudentAttendance(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStudentAttendance_ReturnsNotFound_WhenNotDeleted()
        {
            _mockService.Setup(s => s.DeleteStudentAttendanceAsync(999)).ReturnsAsync(false);

            var result = await _controller.DeleteStudentAttendance(999);

            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Staff Attendance

        [Fact]
        public async Task GetAllStaffAttendance_ReturnsOk_WithList()
        {
            _mockService.Setup(s => s.GetAllStaffAttendanceAsync())
                .ReturnsAsync(new List<StaffAttendance> { new StaffAttendance { StaffAttendanceId = 1 } });

            var result = await _controller.GetAllStaffAttendance();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<StaffAttendance>>(okResult.Value);
            Assert.Single(list);
        }

        [Fact]
        public async Task GetStaffAttendance_ReturnsOk_WhenExists()
        {
            _mockService.Setup(s => s.GetStaffAttendanceByIdAsync(1))
                .ReturnsAsync(new StaffAttendance { StaffAttendanceId = 1 });

            var result = await _controller.GetStaffAttendance(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var att = Assert.IsType<StaffAttendance>(okResult.Value);
            Assert.Equal(1, att.StaffAttendanceId);
        }

        [Fact]
        public async Task GetStaffAttendance_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetStaffAttendanceByIdAsync(999)).ReturnsAsync((StaffAttendance?)null);

            var result = await _controller.GetStaffAttendance(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateStaffAttendance_ReturnsCreatedAtAction()
        {
            var att = new StaffAttendance { StaffAttendanceId = 1 };
            _mockService.Setup(s => s.AddStaffAttendanceAsync(att)).ReturnsAsync(att);

            var result = await _controller.CreateStaffAttendance(att);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdAtt = Assert.IsType<StaffAttendance>(createdResult.Value);
            Assert.Equal(1, createdAtt.StaffAttendanceId);
        }

        [Fact]
        public async Task UpdateStaffAttendance_ReturnsOk_WhenUpdated()
        {
            var att = new StaffAttendance { StaffAttendanceId = 1 };
            _mockService.Setup(s => s.UpdateStaffAttendanceAsync(att)).ReturnsAsync(att);

            var result = await _controller.UpdateStaffAttendance(1, att);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updated = Assert.IsType<StaffAttendance>(okResult.Value);
            Assert.Equal(1, updated.StaffAttendanceId);
        }

        [Fact]
        public async Task UpdateStaffAttendance_ReturnsBadRequest_WhenIdMismatch()
        {
            var att = new StaffAttendance { StaffAttendanceId = 2 };

            var result = await _controller.UpdateStaffAttendance(1, att);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("ID mismatch", badRequest.Value);
        }

        [Fact]
        public async Task DeleteStaffAttendance_ReturnsNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteStaffAttendanceAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteStaffAttendance(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStaffAttendance_ReturnsNotFound_WhenNotDeleted()
        {
            _mockService.Setup(s => s.DeleteStaffAttendanceAsync(999)).ReturnsAsync(false);

            var result = await _controller.DeleteStaffAttendance(999);

            Assert.IsType<NotFoundResult>(result);
        }

        #endregion
    }
}
