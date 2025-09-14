using RSMS.Common.Models;

namespace RSMS.Services.Interfaces
{
    public interface IReportsService
    {
        Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request);
    }
}
