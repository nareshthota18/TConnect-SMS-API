using RSMS.Data;
using RSMS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Implementation
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly RSMSDbContext _context;

        public ActivitiesRepository(RSMSDbContext context)
        {
            _context = context;
        }
    }
}
