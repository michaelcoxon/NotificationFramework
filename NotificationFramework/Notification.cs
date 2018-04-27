using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class Notification
    {
       public INotificationContact Sender { get; set; }

        public INotificationContent Content { get; set; }

        public ICollection<INotificationDelivery> Deliveries { get; }

        public Notification()
        {
            this.Deliveries = new List<INotificationDelivery>();
        }
    }
}
