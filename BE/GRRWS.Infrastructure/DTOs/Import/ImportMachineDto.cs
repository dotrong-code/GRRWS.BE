using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Import
{
    public class ImportMachineDto
    {
        public string? MachineName { get; set; }
        public string? MachineCode { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Specifications { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
