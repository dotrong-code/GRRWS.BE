using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Device : BaseEntity
    {
        public string? DeviceName { get; set; }
        public string? DeviceCode { get; set; }

        // Foreign key 
        public Guid? ZoneId { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? PositionId { get; set; }
        // Navigation
        public Zone? Zone { get; set; }
        public Position? Position { get; set; }
        public Machine? Machine { get; set; }
        public ICollection<DeviceWarranty>? Warranties { get; set; }
        public ICollection<Request>? Requests { get; set; }
    }
}
