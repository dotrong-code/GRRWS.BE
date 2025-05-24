using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class TaskByReportResponse
    {
        public Guid TaskId { get; set; }
        public string TaskType { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public string AssigneeName { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
