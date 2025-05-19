using GRRWS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestDTO
    {
        public Guid Id { get; set; }
        public Guid? ReportId { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public List<IssueDTO> Issues { get; set; } = new();
    }
    public class CreateRequestDTO
    {
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }

        public List<Guid> IssueIds { get; set; } = new();
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
        public string? IssueTitle { get; set; }
    }
}
