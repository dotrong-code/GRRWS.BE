using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class ActionConfirmationDetail
    {
        public Guid ConfirmationId { get; set; }
        public string? ConfirmationDescription { get; set; }
        public bool AssigneeConfirm { get; set; }
        public bool StockKeeperConfirm { get; set; }
        public string ActionType { get; set; } // e.g., StockOut, StockIn, Installation, WarrantySubmission
    }
}
