using GRRWS.Domain.Common;


namespace GRRWS.Domain.Entities
{
    public abstract class BaseEntity
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = TimeHelper.GetHoChiMinhTime();
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = TimeHelper.GetHoChiMinhTime();
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
