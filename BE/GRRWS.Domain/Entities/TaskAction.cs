using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class TaskAction : BaseEntity
    {
        public Guid TaskId { get; set; }                       
        public Guid ErrorActionId { get; set; }                   
        public Guid PerformedById { get; set; }                 
        public DateTime PerformedAt { get; set; }                 
        public string? Notes { get; set; }                        
        public bool IsSuccessful { get; set; }                    

        // Navigation
        public Tasks Task { get; set; }
        public ErrorAction ErrorAction { get; set; }
        public User PerformedBy { get; set; }
    }
}
