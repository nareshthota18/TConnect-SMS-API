using RSMS.Data.Models.LookupEntities;

namespace RSMS.Data.Models.InventoryEntities
{
    public class PurchaseInvoice : BaseEntity
    {
        public long PurchaseId { get; set; }
        public long? SupplierId { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string? Notes { get; set; }

        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
