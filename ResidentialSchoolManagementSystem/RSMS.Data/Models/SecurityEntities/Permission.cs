﻿namespace RSMS.Data.Models.SecurityEntities
{

    public class Permission : BaseEntity
    {
        public int PermissionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
