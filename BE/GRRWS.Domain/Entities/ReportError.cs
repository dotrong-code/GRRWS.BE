using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GRRWS.Domain.Entities
{
    public class ReportError : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Guid ErrorId { get; set; }

        public Report Report { get; set; }
        public Error Error { get; set; }
    }
}
