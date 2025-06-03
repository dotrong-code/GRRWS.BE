using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Tasks : BaseEntity
    {
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskType { get; set; }
        public int? Priority { get; set; } // 1 = Low, 2 = Medium, 3 = High
        public string? Status { get; set; } // e.g., Pending, InProgress, Completed
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; } // Deadline
        public DateTime? EndTime { get; set; }
        public Guid AssigneeId { get; set; }
        // Warranty appointment report fields
        public DateTime? DeviceReturnTime { get; set; } // Time device is returned
        public string? DeviceCondition { get; set; } // Condition after repair
        public string? ReportNotes { get; set; } // Additional report details
        // Navigation
        public User Assignee { get; set; }
        public ICollection<ErrorDetail> ErrorDetails { get; set; }
        public ICollection<RepairSparepart>? RepairSpareparts { get; set; }
        public ICollection<TechnicalSymptomReport>? TechnicalSymptomReports { get; set; }
        public ICollection<TaskAction>? TaskActions { get; set; }
    }
}
