using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models.LookupEntities
{
    public class RSHostel : BaseEntity
    {
        public int RSHId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
