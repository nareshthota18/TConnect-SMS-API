using RSMS.Data.Models.LookupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Common.DTO
{
    public class ConsumptionConfigDTO
    {
        public Guid? Id { get; set; }
        public Guid RSHostelId { get; set; }
        public Guid GradeId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string Frequency { get; set; }   
        public string GradeName { get; set; }
        public string ItemName { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public bool IsActive { get; set; }
    }
}
