using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models.SecurityEntities
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? StaffId { get; set; }
        public string? ExternalId { get; set; }
        public bool IsActive { get; set; } = true;

        public Staff? Staff { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
