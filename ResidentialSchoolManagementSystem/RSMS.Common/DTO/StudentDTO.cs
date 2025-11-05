namespace RSMS.Common.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? RSHostelId { get; set; }
        public Guid? GradeId { get; set; }
        public string Status { get; set; } = "Active";
        public string? ParentName { get; set; }
        public string? ParentContact { get; set; }
        public string? HealthInfo { get; set; }

        
        public string? CategoryName { get; set; }
        public string? RSHostelName { get; set; }
        public string? GradeName { get; set; }
    }
}
