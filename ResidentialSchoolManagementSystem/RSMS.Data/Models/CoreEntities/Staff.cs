using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;

namespace RSMS.Data.Models.CoreEntities
{
    public class Staff : BaseEntity
    {
        public long StaffId { get; set; }
        public string StaffCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public bool IsTeaching { get; set; } = true;
        public string Status { get; set; } = "Active";

        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<StaffAttendance> Attendance { get; set; } = new List<StaffAttendance>();
    }
}
