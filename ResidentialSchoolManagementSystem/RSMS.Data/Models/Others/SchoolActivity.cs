using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.Others
{
    [Table("SchoolActivities", Schema = "rsms")]
    public class SchoolActivity : BaseEntity
    {
        public Guid RSHostelId { get; set; }
        public RSHostel RSHostel { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime ActivityDate { get; set; }
        public string? Category { get; set; }
    }
}
