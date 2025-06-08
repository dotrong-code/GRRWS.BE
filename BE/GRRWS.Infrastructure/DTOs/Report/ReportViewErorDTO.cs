using GRRWS.Infrastructure.DTOs.ErrorDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Report
{
    public class ReportViewErorDTO
    {
        public Guid Id { get; set; }
        public Guid? RequestId { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<ErrorDetailViewDTO>? ErrorDetails { get; set; }
    }
    
}
