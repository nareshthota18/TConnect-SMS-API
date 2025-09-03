using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models
{
    public class StaffAttendance : BaseEntity
    {
        public long StaffAttendanceId { get; set; }
        public long StaffId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        public Staff Staff { get; set; } = default!;
    }

}
