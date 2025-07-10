using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.IssueDTO
{
    public class IssueDTO
    {
        public string? IssueKey { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int? OccurrenceCount { get; set; }
    }
    public class UpdateIssueDTO
    {
        public Guid Id { get; set; }
        public string? IssueKey { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
    }
}
