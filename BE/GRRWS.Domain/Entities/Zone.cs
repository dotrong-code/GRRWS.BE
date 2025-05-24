namespace GRRWS.Domain.Entities
{
    public class Zone : BaseEntity
    {
        public string? ZoneName { get; set; }
        //public string? ZoneCode { get; set; } // e.g., A52-1, A52-2, etc.
        public Guid? AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }
}
