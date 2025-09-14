using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;

namespace RSMS.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _repository;

        public ReportsService(IReportsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request) =>
            await _repository.GetAllAttendanceTimeRange(request);
    }
}
