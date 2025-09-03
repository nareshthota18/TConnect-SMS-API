using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models.LookupEntities
{
    public class Grade : BaseEntity
    {
        public int GradeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
