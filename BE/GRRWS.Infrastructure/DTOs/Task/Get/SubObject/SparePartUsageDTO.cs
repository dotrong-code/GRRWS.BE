namespace GRRWS.Infrastructure.DTOs.Task.Get.SubObject
{
    public class SparePartUsageDTO
    {
        public Guid SparePartId { get; set; } // Required
        public string SparePartName { get; set; } = string.Empty; // Required
        public int QuantityUsed { get; set; }
        public bool IsTakenFromStock { get; set; }
    }
}
