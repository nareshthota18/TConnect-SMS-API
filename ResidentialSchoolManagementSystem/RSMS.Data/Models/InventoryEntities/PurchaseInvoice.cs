using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("PurchaseInvoices", Schema = "rsms")]
    public class PurchaseInvoice : BaseEntity
    {
        public Guid? SupplierId { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string? Notes { get; set; }
        public Guid RSHostelId { get; set; }

        public RSHostel RSHostel { get; set; }

        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
    }
}
