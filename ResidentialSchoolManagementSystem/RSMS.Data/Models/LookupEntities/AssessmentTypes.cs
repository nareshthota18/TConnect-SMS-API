using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("AssessmentTypes", Schema = "rsms")]
    public class AssessmentTypes
    {
        [Key]
        public Guid AssessmentTypeID { get; set; }
        public string AssessmentType { get; set; }
    }
}
