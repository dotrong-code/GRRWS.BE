using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class MachineActionConfirmationDTO
    {
        public Guid? Id { get; set; }
        public string? ConfirmationCode { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? RequestedById { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? TaskId { get; set; }
        public string? Status { get; set; }
        public string? ActionType { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Reason { get; set; }
        public Guid? VerificationToken { get; set; }
        public DateTime? TokenExpiration { get; set; }
        public Guid? SignerId { get; set; }
        public string? SignerRole { get; set; }
        public string? SignatureBase64 { get; set; }
        public Guid? AssigneeId { get; set; }
        public bool? MechanicConfirm { get; set; }
        public Guid? ApprovedById { get; set; }
        public bool? StockkeeperConfirm { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid? MachineId { get; set; }
        public string? Notes { get; set; }
        public string? DeviceCondition { get; set; }
        public string? RequestedByName { get; set; }
        public string? AssigneeName { get; set; }
        public string? ApprovedByName { get; set; }
        public string? SignerName { get; set; }
        public string? DeviceName { get; set; }
        public string? MachineName { get; set; }
        public string? TaskName { get; set; }
    }
}
