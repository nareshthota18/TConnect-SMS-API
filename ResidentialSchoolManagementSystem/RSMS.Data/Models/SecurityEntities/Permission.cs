using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("Permissions", Schema = "rsms")]
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
