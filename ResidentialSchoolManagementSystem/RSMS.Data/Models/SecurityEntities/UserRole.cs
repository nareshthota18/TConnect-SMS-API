using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("UserRoles", Schema = "rsms")]
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}
