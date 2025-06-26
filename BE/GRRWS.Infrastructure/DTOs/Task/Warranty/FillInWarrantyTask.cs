namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class FillInWarrantyTask
    {
        public Guid TaskId { get; set; }
        public DateTime WarrantyTime { get; set; }
        public string? Resolution { get; set; }
        public string? ContractNumber { get; set; }
    }
}
