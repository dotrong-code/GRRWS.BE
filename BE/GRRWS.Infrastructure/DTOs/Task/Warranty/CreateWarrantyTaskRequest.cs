namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class CreateWarrantyTaskRequest
    {
        public Guid RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid? TaskGroupId { get; set; } // Optional, can be null
        public Guid DeviceWarrantyId { get; set; } // Required, to link to the specific warranty
        public List<Guid> TechnicalIssueIds { get; set; } = new List<Guid>(); // Required
        // No SparepartIds - warranty tasks don't use spareparts
    }
}
