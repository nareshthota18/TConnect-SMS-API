namespace RSMS.Common.DTO
{
    public class StaffDTO
    {
        public Guid Id { get; set; }
        public string StaffCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public bool IsTeaching { get; set; }
        public string Status { get; set; } = "Active";

        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
    }
}
