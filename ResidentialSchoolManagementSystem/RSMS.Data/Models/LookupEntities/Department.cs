using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("Departments", Schema = "rsms")]
    public class Department : BaseEntity
    {       
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Staff> Staff { get; set; } = new List<Staff>();
    }
}
