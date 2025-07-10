using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorDTO
{
    public class ErrorDTO
    {
        public string? ErrorCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? EstimatedRepairTime { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
        public string? Severity { get; set; }
    }
    public class UpdateErrorDTO
    {
        public Guid Id { get; set; } // Unique identifier for the error
        public string? ErrorCode { get; set; } // Unique
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? EstimatedRepairTime { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
        public string? Severity { get; set; }
    }
}
