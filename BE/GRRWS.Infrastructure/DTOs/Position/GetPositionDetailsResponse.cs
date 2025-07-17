namespace GRRWS.Infrastructure.DTOs.Position
{
    public class GetPositionDetailsResponse
    {
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
        public CurrentDeviceDetails? CurrentDevice { get; set; }
        public CurrentRequestDetails? CurrentRequest { get; set; }
    }

    public class CurrentDeviceDetails
    {
        public Guid DeviceId { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Status { get; set; } // InUse, Temporary, WarrantyOut
    }

    public class CurrentRequestDetails
    {
        public Guid RequestId { get; set; }
        public string Status { get; set; } // InProgress, Done
        public bool IsSolved { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string Note { get; set; }
        public DeviceDetails OldDevice { get; set; }
        public DeviceDetails? TemporaryDevice { get; set; }
        public HandoverDetails Handover { get; set; }
    }

    public class DeviceDetails
    {
        public string Serial { get; set; }
        public string Model { get; set; }
    }

    public class HandoverDetails
    {
        public string Staff { get; set; }
        public string Status { get; set; } // Delivered, Awaiting
    }
}