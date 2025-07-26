namespace GRRWS.Domain.Entities
{
    public class ErrorDetail : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Guid ErrorId { get; set; }

        //// Navigation
        public Guid? TaskId { get; set; }
        public Report Report { get; set; }
        public Error Error { get; set; }
        public Tasks Task { get; set; }
    }
}
