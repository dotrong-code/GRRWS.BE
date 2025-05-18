using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Machine
{
    public class UpdateMachineRequest
    {
        public Guid Id { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string MachineCode { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public string Specifications { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
