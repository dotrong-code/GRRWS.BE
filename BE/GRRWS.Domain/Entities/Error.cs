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
        public string? Solution { get; set; } // Warranty, Repair, Replacement
        public Guid? MachineId { get; set; }

        // Navigation
        public Machine? Machine { get; set; }

        public ICollection<IssueError>? IssueErrors { get; set; }
        public ICollection<ReportError>? ReportErrors { get; set; }
    }
}
