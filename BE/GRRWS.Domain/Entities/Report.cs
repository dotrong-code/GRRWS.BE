namespace GRRWS.Domain.Entities
{
    public class Report : BaseEntity
    {
        
        public Guid? RequestId { get; set; }
        public string? Location { get; set; }
        // Navigation
        public Request? Request { get; set; }
        public ICollection<ErrorDetail>? ErrorDetails { get; set; }
        public ICollection<WarrantyDetail>? WarrantyDetails { get; set; }
        public ICollection<TechnicalSymptomReport>? TechnicalSymptomReports { get; set; }
    }

}
