using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("Users", Schema = "rsms")]
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? StaffId { get; set; }
        public string? ExternalId { get; set; }
        public bool IsActive { get; set; } = true;
        public byte[]? PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[]? PasswordSalt { get; set; } = Array.Empty<byte>();
        public Staff? Staff { get; set; }
        public ICollection<UserHostel> UserHostels { get; set; } = new List<UserHostel>();
    }
}
