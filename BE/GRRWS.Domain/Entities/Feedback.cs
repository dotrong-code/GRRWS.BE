using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        // Foreign key 
        public Guid UserId { get; set; }
        // Navigation
        public User User { get; set; }
    }
}
