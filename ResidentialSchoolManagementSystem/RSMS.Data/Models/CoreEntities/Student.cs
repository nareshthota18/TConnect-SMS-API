using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.CoreEntities
{
    [Table("Students", Schema = "rsms")]
    public class Student : BaseEntity
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentContact { get; set; }
        public Guid? RSHostelId { get; set; }
        public Guid? GradeId { get; set; }
        public string Status { get; set; } = "Active";
        public string? HealthInfo { get; set; }

        public Category? Category { get; set; }
        public RSHostel? RSHostel { get; set; }
        public Grade? Grade { get; set; }
        public ICollection<StudentAttendance> Attendance { get; set; } = new List<StudentAttendance>();
        public ICollection<AssetIssue> AssetIssues { get; set; } = new List<AssetIssue>();
    }

}
