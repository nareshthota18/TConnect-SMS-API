using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Common.DTO
{
    public class StudentAssessmentDTO
    {
        public Guid? Id { get; set; }
        public Guid StudentID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid AssessmentTypeID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public decimal MaxScore { get; set; }
        public decimal ActualScore { get; set; }
        public string Status { get; set; }
    }
}
