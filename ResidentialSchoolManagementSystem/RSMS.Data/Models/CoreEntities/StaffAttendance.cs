using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models
{
    [Table("StaffAttendance", Schema = "rsms")]
    public class StaffAttendance : BaseEntity
    {
        public long StaffId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = "Present";
        public string? Remarks { get; set; }

        public Staff Staff { get; set; } = default!;
    }

}
