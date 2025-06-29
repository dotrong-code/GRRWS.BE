using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class MachineTechnicalSymptomHistoryResponse
    {
        public Guid Id { get; set; }
        public Guid MachineId { get; set; }
        public Guid TechnicalSymptomId { get; set; }
        public string? SymptomCode { get; set; }
        public string? SymptomDescription { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; }
        public string? Notes { get; set; }
    }
}
