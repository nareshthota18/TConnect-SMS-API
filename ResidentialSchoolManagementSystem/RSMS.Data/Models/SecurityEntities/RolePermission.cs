using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("RolePermissions", Schema = "rsms")]
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; } = default!;
        public Permission Permission { get; set; } = default!;
    }
}
