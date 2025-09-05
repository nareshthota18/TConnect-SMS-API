using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("PurchaseItems", Schema = "rsms")]
    public class PurchaseItem : BaseEntity
    {
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public PurchaseInvoice PurchaseInvoice { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }
}
