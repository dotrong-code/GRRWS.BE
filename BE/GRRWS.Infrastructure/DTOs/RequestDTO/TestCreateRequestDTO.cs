using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class TestCreateRequestDTO
    {
        public Guid DeviceId { get; set; }
        public List<Guid>? IssueIds { get; set; } = new();
        public IFormFile? ImageFile { get; set; } // Ảnh chính (gán vào IssueId đầu tiên)
        public List<IFormFile>? AdditionalImageFiles { get; set; } // Các ảnh bổ sung (gán vào IssueId đầu tiên)
    }
}
