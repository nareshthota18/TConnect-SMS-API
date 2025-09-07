using Microsoft.AspNetCore.Mvc;
using Moq;
using RSMS.Api.Controllers;
using RSMS.Business.Contracts;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Tests.Controllers
{
    public class AttendanceControllerTests
    {
        private readonly Mock<IAttendanceRepository> _mockService;
        private readonly AttendanceController _controller;

        public AttendanceControllerTests()
        {
            _mockService = new Mock<IAttendanceRepository>();
            _controller = new AttendanceController(_mockService.Object);
        }

    }
}
