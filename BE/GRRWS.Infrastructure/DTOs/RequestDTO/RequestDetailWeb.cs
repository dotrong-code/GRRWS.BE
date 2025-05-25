namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestDetailWeb
    {
        public Guid RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string Priority { get; set; } // 1: Low, 2: Medium, 3: High
        public string Status { get; set; } // e.g., Pending, Approved, Denied
        public DateTime RequestDate { get; set; } // DateTime in string format
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        //public List<ErrorWeb> Errors { get; set; } = new List<ErrorWeb>();
    }
    public class IssueForRequestDetailWeb
    {
        public Guid IssueId { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; } // e.g., Open, In Progress, Closed

    }


}

