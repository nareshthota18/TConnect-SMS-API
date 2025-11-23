using RSMS.Data.Models.LookupEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
