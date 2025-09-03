using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models.InventoryEntities
{
    public class AssetIssue : BaseEntity
    {
        public long IssueId { get; set; }
        public long StudentId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public string? Remarks { get; set; }

        public Student Student { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }


}
