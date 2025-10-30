using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.SecurityEntities
{
    [Table("UserHostels", Schema = "rsms")]
    public class UserHostel
    {
        public Guid Id { get; set; } // PK column if it exists
        public Guid UserId { get; set; }
        public Guid? RSHostelId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsPrimary { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }


        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [ForeignKey(nameof(RSHostelId))]
        public RSHostel? RSHostel { get; set; }
    }

}
