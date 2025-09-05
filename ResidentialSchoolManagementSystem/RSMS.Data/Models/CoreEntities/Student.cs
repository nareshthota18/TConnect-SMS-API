using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;

namespace RSMS.Data.Models.CoreEntities
{
    public class Student : BaseEntity
    {
        public long StudentId { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? CategoryId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentContact { get; set; }
        public int? RSHId { get; set; }
        public int? GradeId { get; set; }
        public string Status { get; set; } = "Active";
        public string? HealthInfo { get; set; }

        public Category? Category { get; set; }
        public RSHostel? RSHostel { get; set; }
        public Grade? Grade { get; set; }
        public ICollection<StudentAttendance> Attendance { get; set; } = new List<StudentAttendance>();
        public ICollection<AssetIssue> AssetIssues { get; set; } = new List<AssetIssue>();
    }

}
