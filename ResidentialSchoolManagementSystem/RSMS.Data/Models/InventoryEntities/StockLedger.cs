namespace RSMS.Data.Models.InventoryEntities
{
    public class StockLedger : BaseEntity
    {
        public long LedgerId { get; set; }
        public long ItemId { get; set; }
        public DateTime TranDate { get; set; }
        public string TranType { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Reference { get; set; }
        public string? Remarks { get; set; }

        public Item Item { get; set; } = default!;
    }
}
