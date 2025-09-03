using RSMS.Data.Models.LookupEntities;

namespace RSMS.Data.Models.InventoryEntities
{
    public class Item : BaseEntity
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int ItemTypeId { get; set; }
        public string UOM { get; set; } = string.Empty;
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; } = true;

        public ItemType ItemType { get; set; } = default!;
        public ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItem>();
        public ICollection<StockLedger> StockLedgers { get; set; } = new List<StockLedger>();
        public ICollection<AssetIssue> AssetIssues { get; set; } = new List<AssetIssue>();
    }
}
