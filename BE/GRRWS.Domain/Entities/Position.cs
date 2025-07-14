namespace GRRWS.Domain.Entities
{
    public class Position : BaseEntity
    {
        public int Index { get; set; }
        public Guid? ZoneId { get; set; }
        public Guid? DeviceId { get; set; }
        public Zone Zone { get; set; }
        public Device Device { get; set; }
    }
}
