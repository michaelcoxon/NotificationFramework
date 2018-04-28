using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class Notification
    {
        public INotificationContact Sender { get; }

        public INotificationContent Content { get; }

        public ICollection<NotificationAttachment> Attachments { get; }

        public ICollection<INotificationDelivery> Deliveries { get; }

        public Notification(INotificationContact sender, INotificationContent content)
        {
            this.Sender = sender;
            this.Content = content;
            this.Deliveries = new List<INotificationDelivery>();
            this.Attachments = new List<NotificationAttachment>();
        }
    }
}
