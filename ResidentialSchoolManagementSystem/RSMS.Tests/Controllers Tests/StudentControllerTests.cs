using Moq;
using RSMS.Api.Controllers;
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

       
    }
}
