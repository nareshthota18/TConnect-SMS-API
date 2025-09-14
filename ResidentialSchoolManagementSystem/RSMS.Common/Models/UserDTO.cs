namespace RSMS.Common.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? StaffId { get; set; }
        public string? ExternalId { get; set; }
        public bool IsActive { get; set; }
    }
}
