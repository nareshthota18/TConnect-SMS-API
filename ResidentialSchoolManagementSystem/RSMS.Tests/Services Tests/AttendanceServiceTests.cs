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

      
    }
}
