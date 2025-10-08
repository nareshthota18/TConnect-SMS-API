using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.CoreEntities
{
    [Table("Staff", Schema = "rsms")]
    public class Staff : BaseEntity
    {
        public string StaffCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public bool IsTeaching { get; set; } = true;
        public string Status { get; set; } = "Active";
        public Guid RSHostelId { get; set; }

        public RSHostel RSHostel { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<StaffAttendance> Attendance { get; set; } = new List<StaffAttendance>();
    }
}
