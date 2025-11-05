using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IReportsService
    {
        Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request);
    }
}
