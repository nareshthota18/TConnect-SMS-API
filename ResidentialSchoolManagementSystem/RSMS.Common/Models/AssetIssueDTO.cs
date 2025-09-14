namespace RSMS.Common.Models
{
    public class AssetIssueDTO
    {
        public Guid Id { get; set; }         
        public Guid StudentId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public string? Remarks { get; set; }

        
        public string? StudentName { get; set; }
        public string? ItemName { get; set; }
    }
}
