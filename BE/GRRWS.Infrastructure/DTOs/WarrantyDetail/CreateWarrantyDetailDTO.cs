using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.WarrantyDetail
{
    public class CreateWarrantyDetailDTO
    {
        public string WarrantyNotes { get; set; }
        public Guid RequestId { get; set; } // Để tạo Report
        public string Location { get; set; } // Để tạo Report
        public List<Guid> IssueIds { get; set; } // Các Issue liên quan
    }
}
