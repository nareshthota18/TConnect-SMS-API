namespace RSMS.Data.Models.InventoryEntities
{
    public class PurchaseInvoiceItem : BaseEntity
    {
        public long PurchaseItemId { get; set; }
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public PurchaseInvoice PurchaseInvoice { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }
}
