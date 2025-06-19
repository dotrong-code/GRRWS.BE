using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class MechanicShift : BaseEntity
    {
        public Guid? MechanicId { get; set; }
        public Guid? ShiftId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public Shift? Shift { get; set; }
        public User? Mechanic { get; set; }
        public Tasks? Task { get; set; }
    }
}
