using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Report : BaseEntity
    {
        
        public Guid RequestId { get; set; }
        
        

        // Navigation
        public Request Request { get; set; }
        public ICollection<ReportError>? ReportErrors { get; set; }
    }

}
