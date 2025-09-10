using RSMS.Data;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly RSMSDbContext _context;

        public ReportsService(RSMSDbContext context)
        {
            _context = context;
        }
    }
}
