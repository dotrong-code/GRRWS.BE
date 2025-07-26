namespace GRRWS.Domain.Entities
{
    public class SparePartUsage : BaseEntity
    {
        public Guid? MachineActionConfirmationId { get; set; } // Liên kết với MachineActionConfirmation
        public Guid SparePartId { get; set; }
        public int QuantityUsed { get; set; }
        public bool? IsEnough { get; set; }       
        // Navigation properties
        public Sparepart SparePart { get; set; }
        public MachineActionConfirmation? MachineActionConfirmation { get; set; }
    }
}