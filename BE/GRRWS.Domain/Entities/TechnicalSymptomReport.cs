using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class TechnicalSymptomReport
    {
        public Guid ReportId { get; set; }
        public Guid TechnicalSymptomId { get; set; }
        public Guid? TaskId { get; set; }

        // Navigation
        public Report Report { get; set; }
        public TechnicalSymptom TechnicalSymptom { get; set; }
        public Tasks Task { get; set; }
    }
}
