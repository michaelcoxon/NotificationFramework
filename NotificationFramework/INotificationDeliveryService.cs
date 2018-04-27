using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework.Email
{
    public interface INotificationDeliveryService
    {
        Task DeliverAsync(Notification notification);

        Task<bool> CanDeliverAsync(Notification notification);
    }
}
