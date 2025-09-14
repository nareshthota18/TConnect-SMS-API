using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Business.Contracts
{
    public interface IReportsRepository
    {
        Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request);
    }
}
