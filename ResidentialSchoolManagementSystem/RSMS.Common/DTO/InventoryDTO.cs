namespace RSMS.Common.DTO
{
    public class InventoryDTO
    {
        public Guid Id { get; set; }
        public Guid RSHostelId { get; set; }
        public Guid ItemId { get; set; }
        public Guid ItemTypeId { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal QuantityReceived { get; set; }
        public decimal QuantityIssued { get; set; }
        public decimal QuantityInHand { get; set; }
    }

}
