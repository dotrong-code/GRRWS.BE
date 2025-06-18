using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class TaskGroup : BaseEntity
    {
        public Guid? ReportId { get; set; } // Reference to the report this task group belongs to
        public TaskType GroupType { get; set; } // Warranty, Repair, Replacement, etc.
        public string? GroupName { get; set; } // Name of the task group
        public Report? Report { get; set; } // Navigation property to the report
        public List<Tasks> Tasks { get; set; } = new List<Tasks>(); // List of tasks in this group
    }
}
