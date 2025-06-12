using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class TaskGroup : BaseEntity
    {
        public TaskType GroupType { get; set; } // Warranty, Repair, Replacement, etc.
        public string? GroupName { get; set; } // Name of the task group
        public List<Tasks> Tasks { get; set; } = new List<Tasks>(); // List of tasks in this group
    }
}
