using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface INotification
    {
        INotificationContent Content { get; }

        ICollection<INotificationDelivery> Deliveries { get; }
    }
}
