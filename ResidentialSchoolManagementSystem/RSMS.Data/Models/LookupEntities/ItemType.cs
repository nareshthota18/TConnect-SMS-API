using RSMS.Data.Models.InventoryEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMS.Data.Models.LookupEntities
{
    [Table("ItemTypes", Schema = "rsms")]
    public class ItemType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
