namespace GRRWS.Infrastructure.DTOs.Position
{
    public class GetPositionByAreaResponse
    {
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
        public CurrentDeviceDetails? CurrentDevice { get; set; }
        public CurrentRequestDetails? CurrentRequest { get; set; }
    }

    public class CurrentDeviceDetails
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public bool IsUnderWarranty { get; set; }
    }

    public class CurrentRequestDetails
    {
        public Guid RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsSolved { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
        public bool? IsNeedSign { get; set; }
    }
}