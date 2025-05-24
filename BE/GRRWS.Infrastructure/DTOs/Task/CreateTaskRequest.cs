using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskRequest
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public int Priority { get; set; } // 1 = Low, 2 = Medium, 3 = High
        public string Status { get; set; } // e.g., Pending, InProgress, Completed
        public DateTime? ExpectedTime { get; set; }
        public Guid AssigneeId { get; set; }
        public List<Guid> ErrorDetailIds { get; set; }
    }
}
