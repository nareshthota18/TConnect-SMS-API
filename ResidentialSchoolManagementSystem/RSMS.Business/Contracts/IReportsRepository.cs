using RSMS.Common.DTO;
using RSMS.Services.Interfaces;

namespace RSMS.Repositories.Contracts
{
    public interface IReportsRepository
    {
        Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request);
    }
}
