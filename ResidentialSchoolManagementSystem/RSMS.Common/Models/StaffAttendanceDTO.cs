namespace RSMS.Common.Models
{
    public class StaffAttendanceDTO
    {
        public Guid StaffAttendanceId { get; set; }
        public Guid StaffId { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        public string? StaffName { get; set; } 
    }

}
