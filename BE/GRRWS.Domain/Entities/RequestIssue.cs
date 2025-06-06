﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class RequestIssue : BaseEntity
    {
        
        public string? Status { get; set; }

        // Foreign key 
        public Guid RequestId { get; set; }
        public Guid IssueId { get; set; }
        // Navigation
        public Request Request { get; set; }
        public Issue Issue { get; set; }
    }
}
