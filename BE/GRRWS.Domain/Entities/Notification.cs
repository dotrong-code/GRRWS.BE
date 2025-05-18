using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string? Receiver {  get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int? Priority { get; set; }
        public bool? Enabled { get; set; }
    }
}
