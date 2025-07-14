using Microsoft.AspNetCore.Http;

namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestDTO
    {
        public Guid Id { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public int? PositionIndex { get; set; }
        public string ZoneName { get; set; }
        public string AreaName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsSovled { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }

        public List<IssueDTO> Issues { get; set; } = new();
    }
    public class CreateRequestDTO
    {
        public Guid DeviceId { get; set; }
        public List<Guid>? IssueIds { get; set; } = new();
        public List<IssueImageEntry>? IssueImages { get; set; } = new(); // List of issue-image pairs
    }

    public class UpdateRequestDTO
    {
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }

        public List<Guid> IssueIds { get; set; } = new();
    }
    public class IssueDTO
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public List<string>? ImageUrls { get; set; }
        public string? Status { get; set; }

        public bool IsRejected { get; set; } // Thêm trường này
        public string? RejectionReason { get; set; } // Thêm trường này
        public string? RejectionDetails { get; set; } // Thêm trường này
    }

    public class IssueImageEntry
    {
        public Guid IssueId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class CreateRequestFormDTO
    {
        public Guid DeviceId { get; set; }
        public List<Guid> IssueIds { get; set; } = new();
        public List<IssueImageEntry> IssueImages { get; set; } = new(); // Same structure for form data

        public class CancelRequestDTO
        {
            public Guid RequestId { get; set; }
            public string? Reason { get; set; }

        }
    }
}
