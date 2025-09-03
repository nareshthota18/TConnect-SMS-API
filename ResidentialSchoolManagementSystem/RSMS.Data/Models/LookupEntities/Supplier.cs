using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Data.Models.LookupEntities
{
    public class Supplier : BaseEntity
    {
        public long SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? GSTNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
