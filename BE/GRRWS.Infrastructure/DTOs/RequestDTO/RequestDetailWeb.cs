namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestDetailWeb
    {
        public Guid RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string Priority { get; set; } // 1: Low, 2: Medium, 3: High
        public string Status { get; set; } // e.g., Pending, Approved, Denied
        public DateTime RequestDate { get; set; }
        public bool IsWarranty { get; set; }
        public int RemainingWarratyDate { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Location { get; set; }
        public List<IssueForRequestDetailWeb> Issues { get; set; } = new List<IssueForRequestDetailWeb>();
    }
    public class IssueForRequestDetailWeb
    {
        public Guid IssueId { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public List<string>? Images { get; set; }
    }
    public class ErrorForRequestDetailWeb
    {
        public Guid ErrorId { get; set; }
        public string ErrorCode { get; set; }
        public string Name { get; set; }
        public string? Severity { get; set; }
        public string Status { get; set; } // Indicates if the error has been processed
    }
    public class TaskForRequestDetailWeb
    {
        public Guid TaskId { get; set; }
        public string TaskType { get; set; }
        public string Status { get; set; } // e.g., Pending, In Progress, Completed
        public DateTime? StartTime { get; set; }
        public string AssigneeName { get; set; }
        public DateTime? ExpectedTime { get; set; }
        //public int? NumberOfErrors { get; set; } // Number of errors associated with the task

    }
    public class TechnicalIssueForRequestDetailWeb
    {
        public Guid TechnicalIssueId { get; set; }
        public string SymptomCode { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public string Status { get; set; } // Indicates if the technical issue has been processed (Unassigned/Assigned)
    }
}