namespace RSMS.Common.DTO
{
    public class SchoolActivityDTO
    {
        public Guid Id { get; set; }
        public Guid RSHostelId { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime ActivityDate { get; set; }
        public string? Category { get; set; }
    }

}
