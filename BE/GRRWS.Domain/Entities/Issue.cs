using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public Guid IssueId { get; set; }
        public string? IssueKey { get; set; }

        public ICollection<RequestIssue>? RequestIssues { get; set; }
        public ICollection<IssueError>? IssueErrors { get; set; }
    }
}
