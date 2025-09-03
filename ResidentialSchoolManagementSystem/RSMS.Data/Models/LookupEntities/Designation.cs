using RSMS.Data.Models.CoreEntities;

namespace RSMS.Data.Models.LookupEntities
{
    public class Designation : BaseEntity
    {
        public int DesignationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Staff> Staff { get; set; } = new List<Staff>();
    }
}
