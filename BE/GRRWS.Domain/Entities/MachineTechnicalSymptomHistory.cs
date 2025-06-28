using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class MachineTechnicalSymptomHistory : BaseEntity
    {
        public Guid MachineId { get; set; }
        public Guid TechnicalSymptomId { get; set; }
        public DateTime LastOccurredDate { get; set; }
        public int OccurrenceCount { get; set; } // Số lần xảy ra
        public string? Notes { get; set; }

        // Navigation properties
        public Machine? Machine { get; set; }
        public TechnicalSymptom? TechnicalSymptom { get; set; }
    }
}
