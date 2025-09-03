using RSMS.Data.Models.InventoryEntities;

namespace RSMS.Data.Models.LookupEntities
{
    public class ItemType : BaseEntity
    {
        public int ItemTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
