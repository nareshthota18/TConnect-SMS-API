using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Services.Implementations;
using System.Threading.Tasks;
using Xunit;

namespace RSMS.Tests.Services
{
    public class StudentServiceTests
    {
        private RSMSDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RSMSDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new RSMSDbContext(options);
        }

        [Fact]
        public async Task GetStudentById_ShouldReturnStudent_WhenExists()
        {
            var context = GetInMemoryDbContext();
            var student = new Student { StudentId = 1, FirstName = "John Doe" };
            context.Students.Add(student);
            await context.SaveChangesAsync();

            var service = new StudentService(context);
            var result = await service.GetStudentByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result!.FirstName);
        }

        [Fact]
        public async Task GetStudentById_ShouldReturnNull_WhenNotExists()
        {
            var context = GetInMemoryDbContext();
            var service = new StudentService(context);

            var result = await service.GetStudentByIdAsync(999);

            Assert.Null(result);
        }
    }
}
