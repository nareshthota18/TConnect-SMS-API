using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RSMS.Tests.Integration
{
    public class AttendanceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AttendanceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Create a client to send requests to the in-memory test server
            _client = factory.CreateClient();
        }

        #region Student Attendance Endpoints

        [Fact]
        public async Task GetAllStudentAttendance_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/attendance/students");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetStudentAttendance_ReturnsSuccess_ForExistingId()
        {
            // Make sure you have a student attendance with ID = 1 seeded for this test
            var response = await _client.GetAsync("/api/attendance/students/1");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetStudentAttendance_ReturnsNotFound_ForInvalidId()
        {
            var response = await _client.GetAsync("/api/attendance/students/999");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region Staff Attendance Endpoints

        [Fact]
        public async Task GetAllStaffAttendance_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/attendance/staff");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetStaffAttendance_ReturnsSuccess_ForExistingId()
        {
            // Make sure you have a staff attendance with ID = 1 seeded for this test
            var response = await _client.GetAsync("/api/attendance/staff/1");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetStaffAttendance_ReturnsNotFound_ForInvalidId()
        {
            var response = await _client.GetAsync("/api/attendance/staff/999");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion
    }
}
