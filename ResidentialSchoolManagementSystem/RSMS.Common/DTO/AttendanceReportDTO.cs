
namespace RSMS.Services.Interfaces
{
    public class AttendanceReportDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
    }
}