namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class CreateWarrantyReturnTaskRequest
    {
        public Guid RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid DeviceWarrantyId { get; set; } // Required, to link to the specific warranty
    }
}
