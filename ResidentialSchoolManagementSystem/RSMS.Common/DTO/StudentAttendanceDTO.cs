namespace RSMS.Common.DTO
{
    public class StudentAttendanceDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Session { get; set; } = "Morning";
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        
        public string? StudentName { get; set; }
        public string? AdmissionNumber { get; set; }
    }
}
