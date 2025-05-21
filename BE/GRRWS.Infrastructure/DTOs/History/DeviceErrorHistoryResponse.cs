using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class DeviceErrorHistoryResponse
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public Guid ErrorId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; }
        public string Notes { get; set; }
    }
}
