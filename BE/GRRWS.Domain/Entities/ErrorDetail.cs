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
        public Guid? RequestTakeSparePartUsageId { get; set; } // Liên kết 1-1 với RequestTakeSparePartUsage
        public ErrorGuideline? ErrorGuideline { get; set; } // Nullable to allow for cases without a guideline
        public ICollection<ErrorFixProgress>? ProgressRecords { get; set; }
        public RequestTakeSparePartUsage? RequestTakeSparePartUsage { get; set; } // Navigation property 1-1
    }
}
