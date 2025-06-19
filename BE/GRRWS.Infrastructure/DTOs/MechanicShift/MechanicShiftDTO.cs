using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MechanicShift
{
    public class MechanicShiftDTO
    {
        public Guid MechanicId { get; set; }
        public Guid TaskId { get; set; }
    }
    public class MechanicShiftResponseDTO
    {
        public Guid MechanicShiftId { get; set; }
        public Guid MechanicId { get; set; }
        public string MechanicName { get; set; }
        public Guid TaskId { get; set; }
        public string ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}