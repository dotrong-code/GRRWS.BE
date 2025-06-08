namespace GRRWS.Infrastructure.DTOs.Task.Get.SubObject
{
    public class ErrorFixProgressDTO
    {
        public Guid ErrorFixStepId { get; set; }
        public string? StepDescription { get; set; } // Unique
        public int StepOrder { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
