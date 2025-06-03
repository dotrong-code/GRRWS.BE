using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class UpdateTaskRequest
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public Guid AssigneeId { get; set; }
    }
}
