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
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int? Priority { get; set; }
        public bool? Enabled { get; set; } = true;

        // New properties for notification system
        public string? Data { get; set; } // JSON data for additional info
        public NotificationType Type { get; set; } = NotificationType.General;
        public NotificationChannel Channel { get; set; } = NotificationChannel.Both;

        // Navigation properties
        public virtual User Sender { get; set; }
        public virtual ICollection<NotificationReceiver> NotificationReceivers { get; set; } = new List<NotificationReceiver>();
    }
}