using System;
using System.Collections.Generic;

namespace GRRWS.Domain.Entities
{
    public class TechnicalSymptom : BaseEntity
    {
        public string? SymptomCode { get; set; } // Unique
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }

        // Navigation
        public ICollection<IssueTechnicalSymptom>? IssueTechnicalSymptoms { get; set; }
    }
}