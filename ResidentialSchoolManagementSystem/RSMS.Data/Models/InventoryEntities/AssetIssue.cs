using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("AssetIssues", Schema = "rsms")]
    public class AssetIssue : BaseEntity
    {       
        public Guid StudentId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public string? Remarks { get; set; }
        public Guid RSHostelId { get; set; }

        public RSHostel RSHostel { get; set; }
        public Student Student { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }


}
