﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Machine : BaseEntity
    {
        public string? MachineName { get; set; }
        public string? MachineCode { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // Active, Discontinued
        public DateTime? ReleaseDate { get; set; }
        public string? Specifications { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<Device>? Devices { get; set; }   
        public ICollection<MachineSparepart>? MachineSpareparts { get; set; }
    }
}
