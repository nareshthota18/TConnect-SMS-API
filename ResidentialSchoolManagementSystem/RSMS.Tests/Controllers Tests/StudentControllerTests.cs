using Microsoft.AspNetCore.Mvc;
using Moq;
using RSMS.Api.Controllers;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentService> _mockService;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockService = new Mock<IStudentService>();
            _controller = new StudentsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllStudentsAsync())
                        .ReturnsAsync(new List<Student> { new Student { StudentId = 1, FirstName = "John" } });

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var students = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Single(students);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenStudentExists()
        {
            _mockService.Setup(s => s.GetStudentByIdAsync(1))
                        .ReturnsAsync(new Student { StudentId = 1, FirstName = "John" });

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var student = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, student.StudentId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            _mockService.Setup(s => s.GetStudentByIdAsync(999)).ReturnsAsync((Student?)null);

            var result = await _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var student = new Student { StudentId = 1, FirstName = "John" };
            _mockService.Setup(s => s.AddStudentAsync(student)).ReturnsAsync(student);

            var result = await _controller.Create(student);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdStudent = Assert.IsType<Student>(createdResult.Value);
            Assert.Equal("John", createdStudent.FirstName);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenStudentUpdated()
        {
            var student = new Student { StudentId = 1, FirstName = "John" };
            _mockService.Setup(s => s.UpdateStudentAsync(student)).ReturnsAsync(student);

            var result = await _controller.Update(1, student);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedStudent = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, updatedStudent.StudentId);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var student = new Student { StudentId = 2, FirstName = "John" };

            var result = await _controller.Update(1, student);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("ID mismatch", badRequest.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenStudentDeleted()
        {
            _mockService.Setup(s => s.DeleteStudentAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            _mockService.Setup(s => s.DeleteStudentAsync(999)).ReturnsAsync(false);

            var result = await _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
