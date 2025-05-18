using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Machine
{
    public class GetMachineResponse
    {
        public Guid Id { get; set; }
        public string MachineName { get; set; }
        public string MachineCode { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Specifications { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<Guid>? DeviceIds { get; set; }
    }
}
