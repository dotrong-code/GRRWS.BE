using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorDetail
{
    public class ErrorDetailViewDTO
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public Guid ErrorId { get; set; }
        public string? ErrorName { get; set; } // Giả định Error có thuộc tính ErrorName
        // Thêm các thuộc tính khác nếu cần
    }
}
