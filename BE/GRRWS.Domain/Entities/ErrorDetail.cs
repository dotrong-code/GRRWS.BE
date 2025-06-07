namespace GRRWS.Domain.Entities
{
    public class ErrorDetail : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Guid ErrorId { get; set; }
        public Guid? ErrorGuideLineId { get; set; }
        public Guid? TaskId { get; set; }
        public Report Report { get; set; }
        public Error Error { get; set; }
        public Tasks Task { get; set; }
        public ErrorGuideline? ErrorGuideline { get; set; } // Nullable to allow for cases without a guideline
        public ICollection<ErrorFixProgress>? ProgressRecords { get; set; }
        public ICollection<SparePartUsage>? SparePartUsages { get; set; } // Collection of spare parts used for this error
    }
}
