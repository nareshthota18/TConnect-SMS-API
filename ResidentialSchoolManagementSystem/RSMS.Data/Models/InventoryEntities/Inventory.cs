using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("Inventory", Schema = "rsms")]
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid RSHostelId { get; set; }
        public Guid ItemId { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal QuantityReceived { get; set; }
        public decimal QuantityIssued { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal QuantityInHand { get; private set; }

        public RSHostel RSHostel { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }

}
