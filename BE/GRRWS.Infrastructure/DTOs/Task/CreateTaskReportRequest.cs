using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskReportRequest
    {
        public Guid TaskId { get; set; }
        public DateTime DeviceReturnTime { get; set; }
        public string DeviceCondition { get; set; } // e.g., Repaired, Partially Repaired, Unrepaired
        public string ReportNotes { get; set; }
    }
}
