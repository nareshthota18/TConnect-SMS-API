using Microsoft.AspNetCore.Mvc;
using Moq;
using RSMS.Api.Controllers;
using RSMS.Business.Contracts;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Interfaces;

namespace RSMS.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentRepository> _mockService;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockService = new Mock<IStudentRepository>();
            _controller = new StudentsController(_mockService.Object);
        }

       
    }
}
