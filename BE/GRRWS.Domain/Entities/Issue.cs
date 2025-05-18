using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string? IssueKey { get; set; } // Mã triệu chứng
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int? OccurrenceCount { get; set; }
        // Navigation
        public ICollection<RequestIssue>? RequestIssues { get; set; }
        public ICollection<IssueError>? IssueErrors { get; set; }
    }
}
