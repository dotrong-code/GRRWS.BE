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
        public int? Frequency { get; set; } // Mức độ Phổ Biến
        public string? Severity { get; set; } // Low, Medium, High
        public Guid? MachineId { get; set; }

        // Navigation
        public Machine? Machine { get; set; }

        public ICollection<RequestIssue>? RequestIssues { get; set; }
        public ICollection<IssueError>? IssueErrors { get; set; }
    }
}
