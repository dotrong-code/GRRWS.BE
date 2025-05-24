namespace GRRWS.Domain.Entities
{
    public class Area : BaseEntity
    {
        public string? AreaName { get; set; }
        public string? AreaCode { get; set; } // e.g., NVT, A52, etc.
        public ICollection<Zone>? Zones { get; set; }
    }
}
