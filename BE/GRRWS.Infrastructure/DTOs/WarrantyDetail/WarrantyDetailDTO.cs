using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.WarrantyDetail
{
    public class WarrantyDetailDTO
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public Guid? TaskId { get; set; }
        public string WarrantyNotes { get; set; }
        public List<Guid> IssueIds { get; set; }
    }
}
