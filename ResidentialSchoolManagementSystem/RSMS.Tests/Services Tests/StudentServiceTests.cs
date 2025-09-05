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

      
    }
}
