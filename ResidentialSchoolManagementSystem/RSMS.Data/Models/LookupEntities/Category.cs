using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("Categories", Schema = "rsms")]
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
