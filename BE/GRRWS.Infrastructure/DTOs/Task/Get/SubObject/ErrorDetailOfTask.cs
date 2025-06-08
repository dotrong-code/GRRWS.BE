namespace GRRWS.Infrastructure.DTOs.Task.Get.SubObject
{
    public class ErrorDetailOfTask
    {
        public Guid ErrorId { get; set; }
        public Guid ErrorDetailId { get; set; }
        public string? ErrorName { get; set; }
        public string? ErrorGuildelineTitle { get; set; }
        public List<ErrorFixProgressDTO> ErrorFixProgress { get; set; } = new List<ErrorFixProgressDTO>();
        public List<SparePartUsageDTO> SparePartUsages { get; set; } = new List<SparePartUsageDTO>();
    }
}
