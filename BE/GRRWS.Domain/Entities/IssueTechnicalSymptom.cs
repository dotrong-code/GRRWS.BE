using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class IssueTechnicalSymptom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IssueId { get; set; }
        public Guid TechnicalSymptomId { get; set; }

        // Navigation
        public Issue Issue { get; set; }
        public TechnicalSymptom TechnicalSymptom { get; set; }
    }
}
