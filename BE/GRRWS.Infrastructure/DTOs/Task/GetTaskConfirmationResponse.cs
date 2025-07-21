using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class GetTaskConfirmationResponse
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public Guid? SignerId { get; set; }
        public Guid? DeviceId { get; set; }
        public string SignerName { get; set; }
        public string SignerRole { get; set; }
        public string SignatureBase64 { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceCondition { get; set; }
        public string ConfirmationType { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
