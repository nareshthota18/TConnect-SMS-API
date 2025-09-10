using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IReportsRepository
    {
        public Task<ReportRequestDTO?> GetAllAttendanceTimeRange(ReportRequestDTO att);
    }
}
