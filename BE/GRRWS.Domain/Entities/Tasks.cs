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
        public int? Priority { get; set; } //1 = Low, 2 = Medium, 3 = High
        public string? Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; } //Deadline
        public DateTime? EndTime { get; set; }
        public Guid AssigneeId { get; set; }
        public User Assignee { get; set; }
        public ICollection<ErrorDetail> ErrorDetails { get; set; }
        public ICollection<RepairSparepart>? RepairSpareparts { get; set; }
    }
}
