namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateSimpleTaskRequest
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; } = "Replace";
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        
        // Device replacement details
        public Guid DeviceToRemoveId { get; set; } // Device to be brought to repair place
        public Guid? ReplacementDeviceId { get; set; } // New device to set up (optional if not yet assigned)
        public string? TaskDescription { get; set; } // Additional notes
        
        // Location details
        public string InstallationLocation { get; set; } // Where the replacement will happen
        
        // Replacement actions
        public bool BringDeviceToRepairPlace { get; set; } = true; // Action 1
        public bool SetupReplacementDevice { get; set; } = true; // Action 2
    }
}