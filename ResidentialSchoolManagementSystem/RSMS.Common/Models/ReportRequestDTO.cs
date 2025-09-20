using RSMS.Common.Enums;

namespace RSMS.Common.Models
{
    public class ReportRequestDTO
    {
        public int Id { get; set; }
        public TimeRange timeRange { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
