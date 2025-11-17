namespace RSMS.Common.DTO
{
    public class SchoolHolidayDTO
    {
        public Guid Id { get; set; }

        public Guid RSHostelId { get; set; }
        public string HostelName { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int NumberOfDays { get; set; }

        public string Description { get; set; }
    }
}
