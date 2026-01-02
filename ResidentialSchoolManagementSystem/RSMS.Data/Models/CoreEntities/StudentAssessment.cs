using RSMS.Data.Models.LookupEntities;
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
        public Guid StudentId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public decimal MaxScore { get; set; }
        public decimal ActualScore { get; set; }
        public string Status { get; set; }
        public Student Student { get; set; }
        public Department Department { get; set; }
    }
}
