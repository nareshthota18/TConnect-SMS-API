using RSMS.Data.Models.CoreEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("AssetIssues", Schema = "rsms")]
    public class AssetIssue : BaseEntity
    {       
        public long StudentId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public string? Remarks { get; set; }

        public Student Student { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }


}
