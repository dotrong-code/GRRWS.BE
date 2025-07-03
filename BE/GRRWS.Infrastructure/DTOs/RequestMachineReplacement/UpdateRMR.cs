namespace GRRWS.Infrastructure.DTOs.RequestMachineReplacement
{
    public class UpdateRMR
    {
        public Guid RequestMachineId { get; set; }
        public string? Reason { get; set; } // Lý do thay thế
        public string? Notes { get; set; } // Ghi chú
        public Guid? DeviceId { get; set; } // Optional mechanic ID if applicable
    }
}
