using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class WarrantyDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ReportId { get; set; }
        public Guid? TaskId { get; set; }
        public string? WarrantyNotes { get; set; } // Thông tin bảo hành

        // Navigation
        public Report Report { get; set; }
        public Tasks Task { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
