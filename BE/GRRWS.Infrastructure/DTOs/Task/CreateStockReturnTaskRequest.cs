using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateStockReturnTaskRequest
    {
        public Guid RequestId { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? TaskGroupId { get; set; }
        public Guid? DeviceId { get; set; } // DeviceId của máy thay thế cần trả về kho
    }
}
