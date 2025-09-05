using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Implementations;

namespace RSMS.Tests.Services
{
    public class AttendanceServiceTests
    {
        private RSMSDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RSMSDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new RSMSDbContext(options);
        }

        [Fact]
        public async Task GetAttendanceById_ShouldReturnAttendance_WhenExists()
        {
            var context = GetInMemoryDbContext();
            var attendance = new StudentAttendance { AttendanceId = 1, StudentId = 1, AttendanceDate = DateTime.Today };
            context.StudentAttendance.Add(attendance);
            await context.SaveChangesAsync();

            var service = new AttendanceService(context);
            var result = await service.GetStudentAttendanceByIdAsync(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAttendanceById_ShouldReturnNull_WhenNotExists()
        {
            var context = GetInMemoryDbContext();
            var service = new AttendanceService(context);

            var result = await service.GetStudentAttendanceByIdAsync(999);

            Assert.Null(result);
        }
    }
}
