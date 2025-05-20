using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Device.ErrorHistory
{
    public class DeviceErrorHistoryDTO
    {
        public Guid DeviceId { get; set; }
        public Guid ErrorId { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; } // Số lần xảy ra
        public string? Notes { get; set; }
    }
}
