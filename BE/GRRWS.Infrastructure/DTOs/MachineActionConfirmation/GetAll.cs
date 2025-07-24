using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class GetAll
    {
        public Guid Id { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid RequestedById { get; set; }
        public Guid? AssigneeId { get; set; }
        public string? AssigneeName { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? MachineId { get; set; }
        public string Status { get; set; }
        public string ActionType { get; set; }
        public bool MechanicConfirm { get; set; }
        public bool StockkeeperConfirm { get; set; }
        public string? Notes { get; set; }
    }
}
