namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetDetailUninstallTaskForMechanic : GetTaskDetailBase
    {
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string Location { get; set; }
        public string? TaskGroupName { get; set; }
        public List<TaskConfirmationResponeDTO>? TaskConfirmations { get; set; }
    }
}