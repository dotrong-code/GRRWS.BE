namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetDetailInstallTaskForMechanic : GetTaskDetailBase
    {
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string Location { get; set; }
        public string? TaskGroupName { get; set; }
        public bool IsUninstall { get; set; } // True if this is an uninstall task, false if it's an install task
        public bool IsInstall { get; set; } // True if this is an uninstall task, false if it's an install task
        public bool IsSigned { get; set; } // True if this is an uninstall task, false if it's an install task
        public Guid NewDeviceId { get; set; } // ID of the new device to be installed

        public List<TaskConfirmationResponseDTO>? TaskConfirmations { get; set; }
    }

}