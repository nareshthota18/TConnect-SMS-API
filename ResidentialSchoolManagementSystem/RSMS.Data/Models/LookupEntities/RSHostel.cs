using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("RSHostels", Schema = "rsms")]
    public class RSHostel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
