using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid SenderId { get; set; }
        public Guid? ReceiverId { get; set; }
        public string? Receiver { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int? Priority { get; set; }
        public bool? Enabled { get; set; }

        // New properties for notification system
        public string? Data { get; set; } // JSON data for additional info
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public NotificationType Type { get; set; } = NotificationType.General;
        public NotificationChannel Channel { get; set; } = NotificationChannel.Both;

        // Navigation properties
        public virtual User Sender { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}
