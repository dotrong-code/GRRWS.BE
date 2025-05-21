using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class DeviceIssueHistoryResponse
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public Guid IssueId { get; set; }
        public string IssueCode { get; set; }
        public string IssueDescription { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; }
        public string Notes { get; set; }
    }
}
