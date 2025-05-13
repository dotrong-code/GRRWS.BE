using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Area : BaseEntity
    {
        public string? Name { get; set; }
        public string? Instruction { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? KeyPosition { get; set; }

        // Foreign Key
        public Guid? UserId { get; set; }

        // Navigation
        public User? User { get; set; }

    }
}
