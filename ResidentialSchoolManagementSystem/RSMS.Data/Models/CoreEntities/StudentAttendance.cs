namespace RSMS.Data.Models.CoreEntities
{
    public class StudentAttendance : BaseEntity
    {
        public long AttendanceId { get; set; }
        public long StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Session { get; set; } = "Morning";
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        public Student Student { get; set; } = default!;
    }
}
