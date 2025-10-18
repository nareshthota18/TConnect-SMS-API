using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("UserHostels", Schema = "rsms")]
    public class UserHostel
    {
        public Guid UserId { get; set; }
        public Guid RSHostelId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsPrimary { get; set; } = false;

        public User User { get; set; }
        public RSHostel RSHostel { get; set; }
        public Role Role { get; set; }
    }
}
