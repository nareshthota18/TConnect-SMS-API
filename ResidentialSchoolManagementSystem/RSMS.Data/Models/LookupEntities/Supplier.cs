using RSMS.Data.Models.InventoryEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("Suppliers", Schema = "rsms")]
    public class Supplier : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? GSTNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? RSHostelId { get; set; }

        public RSHostel RSHostel { get; set; }

        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
