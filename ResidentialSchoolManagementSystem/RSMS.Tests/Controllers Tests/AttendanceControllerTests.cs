using Moq;
using RSMS.Api.Controllers;
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

    }
}
