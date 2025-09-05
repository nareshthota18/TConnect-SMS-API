using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.CoreEntities
{
    [Table("StudentAttendance", Schema = "rsms")]
    public class StudentAttendance : BaseEntity
    {       
        public Guid StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Session { get; set; } = "Morning";
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        public Student Student { get; set; } = default!;
    }
}
