using RSMS.Common.Enums;

namespace RSMS.Common.Models
{
    public class ReportRequestDTO
    {
        public int Id { get; set; }
        public TimeRange timeRange { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
    }
}
