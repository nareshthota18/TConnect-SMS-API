using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("Items", Schema = "rsms")]
    public class Item : BaseEntity
    {       
        public string ItemCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid ItemTypeId { get; set; }
        public string UOM { get; set; } = string.Empty;
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; } = true;

        public ItemType ItemType { get; set; } = default!;
        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
        public ICollection<StockLedger> StockLedgers { get; set; } = new List<StockLedger>();
        public ICollection<AssetIssue> AssetIssues { get; set; } = new List<AssetIssue>();
    }
}
