namespace RSMS.Common.Models
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid ItemTypeId { get; set; }
        public string UOM { get; set; } = string.Empty;
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; } = true;

        
        public string? ItemTypeName { get; set; }
    }
}
