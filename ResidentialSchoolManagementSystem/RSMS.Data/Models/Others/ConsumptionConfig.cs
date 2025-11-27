using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.Others
{
    [Table("ConsumptionConfig", Schema = "rsms")]
    public class ConsumptionConfig : BaseEntity
    {
        public Guid RSHostelId { get; set; }
        public Guid GradeId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Quantity { get; set; } 
        public string Frequency { get; set; } 
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; } 
        public bool IsActive { get; set; }
        public RSHostel RSHostel { get; set; }
        public Grade Grade { get; set; }
        public Item Item { get; set; }
    }
}
