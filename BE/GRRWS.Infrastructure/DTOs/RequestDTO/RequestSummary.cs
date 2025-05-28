namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestSummary
    {
        public Guid RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string Priority { get; set; } // 1: Low, 2: Medium, 3: High
        public string Status { get; set; } // e.g., Pending, Approved, Denied
        public DateTime RequestDate { get; set; } // DateTime in string format
    }
}
