using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class ErrorDetailDto
    {
        public Guid ErrorId { get; set; }
        public string ErrorCode { get; set; }
    }
}
