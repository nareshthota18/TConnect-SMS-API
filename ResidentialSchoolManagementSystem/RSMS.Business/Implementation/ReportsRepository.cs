using Microsoft.EntityFrameworkCore;
using RSMS.Repositories.Contracts;
using RSMS.Common.Models;
using RSMS.Data;
using RSMS.Services.Interfaces;

namespace RSMS.Repositories.Implementation
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly RSMSDbContext _context;

        public ReportsRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceReportDTO>> GetAllAttendanceTimeRange(ReportRequestDTO request)
        {
            var query = await _context.StudentAttendance
                .Where(a => a.AttendanceDate >= request.StartDate && a.AttendanceDate <= request.EndDate)
                .Select(a => new AttendanceReportDTO
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    AttendanceDate = a.AttendanceDate,
                    Status = a.Status
                })
                .ToListAsync();

            return query;
        }
    }
}
