using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.Others
{
    [Table("SchoolHolidays", Schema = "rsms")]
    public class SchoolHoliday : BaseEntity
    {
        public Guid RSHostelId { get; set; }
        public RSHostel RSHostel { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Description { get; set; }
    }
}
