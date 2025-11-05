using RSMS.Repositories.Contracts;
using RSMS.Common.DTO;
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
