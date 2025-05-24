using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class UpdateTaskDTO
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid AssigneeId { get; set; }
        public DateTime? DeviceReturnTime { get; set; }
        public string DeviceCondition { get; set; }
        public string ReportNotes { get; set; }
        public Guid ModifiedBy { get; set; }
        public List<Guid> ErrorIds { get; set; }
        public List<Guid> SparepartIds { get; set; }
    }
}
