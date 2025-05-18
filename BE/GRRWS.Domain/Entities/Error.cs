using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Error : BaseEntity
    {
        public string? ErrorCode { get; set; } // Unique
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? EstimatedRepairTime { get; set; }
        public string? Sparepart { get; set; }
        public string? Solution { get; set; }
        public Guid? MachineId { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
        public string? Severity { get; set; } // Low, Medium, High
        // Navigation
        public Machine? Machine { get; set; }

        public ICollection<IssueError>? IssueErrors { get; set; }
        public ICollection<ErrorDetail>? ErrorDetails { get; set; }
    }
}
