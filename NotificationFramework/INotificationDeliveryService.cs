using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface INotificationDeliveryService<TNotificationDelivery> where TNotificationDelivery : INotificationDelivery
    {
        Task<NotificationDeliveryAttempt> DeliverAsync(Notification notification, TNotificationDelivery notificationDelivery);
    }
}
