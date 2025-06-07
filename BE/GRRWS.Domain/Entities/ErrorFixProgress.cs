namespace GRRWS.Domain.Entities
{
    public class ErrorFixProgress : BaseEntity
    {

        public Guid ErrorDetailId { get; set; }
        public Guid ErrorFixStepId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        // Navigation properties
        public ErrorDetail ErrorDetail { get; set; }
        public ErrorFixStep ErrorFixStep { get; set; }
    }
}
