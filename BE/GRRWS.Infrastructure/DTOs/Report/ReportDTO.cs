using GRRWS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Report
{
    public class ReportCreateDTO
    {
        public Guid? RequestId { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public List<Guid> ErrorIds { get; set; }
    }

    public class ReportUpdateDTO
    {
        public Guid Id { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public List<Guid> ErrorIds { get; set; }
    }

    public class ReportViewDTO
    {
        public Guid Id { get; set; }
        public Guid? RequestId { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ErrorSummaryDTO> Errors { get; set; }
    }
    public class ErrorSummaryDTO
    {
        public Guid ErrorId { get; set; }
        public string Name { get; set; }
    }
}
