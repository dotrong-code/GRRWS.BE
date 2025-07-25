using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Enum
{
    public enum MachineActionStatus
    {
        Pending,      // Waiting for approval or confirmation
        Approved,     // Approved by stockkeeper or supervisor
        InProgress,   // Action being performed
        Completed,    // Action completed
        Rejected,     // Action rejected
        Cancelled     // Action cancelled
    }
}
