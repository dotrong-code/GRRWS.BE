namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetDetailInstallTaskForMechanic : GetTaskDetailBase
    {
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string Location { get; set; }
        public string? TaskGroupName { get; set; }
    }
}