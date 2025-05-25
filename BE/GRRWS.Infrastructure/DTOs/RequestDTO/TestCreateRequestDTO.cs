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
        public List<Guid> IssueIds { get; set; } = new();
        public List<IFormFile> ImageFiles { get; set; } = new();
        public List<Guid> IssueIdsMatchWithImage { get; set; } = new();
    }
}
