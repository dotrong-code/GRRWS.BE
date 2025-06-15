namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestInfoDto
    {
        public Guid ReportId { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string Location { get; set; }
    }
}
