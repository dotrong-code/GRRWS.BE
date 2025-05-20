using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Device.IssueHistory
{
    public class DeviceIssueHistoryDTO
    {
        public Guid DeviceId { get; set; }
        public Guid IssueId { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; } // Số lần xảy ra
        public string? Notes { get; set; }
    }
}
