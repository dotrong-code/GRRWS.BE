using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class NotificationReceiver
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid NotificationId { get; set; }
        public Guid ReceiverId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
         public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Notification Notification { get; set; }
        public virtual User Receiver { get; set; }
    }

}