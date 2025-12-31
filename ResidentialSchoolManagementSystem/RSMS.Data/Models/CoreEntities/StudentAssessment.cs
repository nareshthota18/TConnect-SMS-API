using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Data.Models.CoreEntities
{
    [Table("StudentAssessment", Schema = "rsms")]
    public class StudentAssessment : BaseEntity
    {
        public Guid AssessmentID { get; set; }
        public Guid StudentID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid AssessmentTypeID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public decimal MaxScore { get; set; }
        public decimal ActualScore { get; set; }
        public string Status { get; set; }
    }
}
