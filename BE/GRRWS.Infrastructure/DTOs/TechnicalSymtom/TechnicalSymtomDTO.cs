using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.TechnicalSymtom
{
    public class TechnicalSymtomDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class TechnicalSymptomViewDTO
    {
        public string? SymptomCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
    }
    public class TechnicalSymptomUpdateDTO
    {
        public Guid Id { get; set; } // Unique identifier for the technical symptom
        public string? SymptomCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
    }
}
