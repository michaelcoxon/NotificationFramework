using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework.Email
{
    public interface IEmailDeliveryService
    {
        Task<INotificationDeliveryAttempt> DeliverAsync(Notification notification, INotificationDelivery notificationDelivery);
    }
}
