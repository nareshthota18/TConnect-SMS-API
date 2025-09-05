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

       
    }
}
