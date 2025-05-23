namespace GRRWS.Domain.Entities
{
    public class Report : BaseEntity
    {

        public Guid? RequestId { get; set; }
        public int Priority { get; set; } //1 = Low, 2 = Medium, 3 = High

        public string Location { get; set; }
        public string Status { get; set; }
        // Navigation
        public Request? Request { get; set; }
        public ICollection<ErrorDetail>? ErrorDetails { get; set; }
    }

}
