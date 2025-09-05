using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.InventoryEntities
{
    [Table("StockLedger", Schema = "rsms")]
    public class StockLedger : BaseEntity
    {       
        public Guid ItemId { get; set; }
        public DateTime TranDate { get; set; }
        public string TranType { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Reference { get; set; }
        public string? Remarks { get; set; }

        public Item Item { get; set; } = default!;
    }
}
