using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.SecurityEntities;
using RSMS.Services.Implementations;

namespace RSMS.Tests.Services
{
    public class RoleServiceTests
    {
        private RSMSDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RSMSDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new RSMSDbContext(options);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRole_WhenRoleExists()
        {
            var context = GetInMemoryDbContext();
            var role = new Role { RoleId = 1, Name = "Admin" };
            context.Roles.Add(role);
            await context.SaveChangesAsync();

            var service = new RoleService(context);
            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Admin", result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenRoleDoesNotExist()
        {
            var context = GetInMemoryDbContext();
            var service = new RoleService(context);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }
    }
}
