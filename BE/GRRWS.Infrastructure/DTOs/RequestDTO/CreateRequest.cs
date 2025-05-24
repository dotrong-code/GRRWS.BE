namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class CreateRequest
    {
        public Guid DeviceId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Priority { get; set; }
        public List<Guid> IssueIds { get; set; } = new();
    }
}
