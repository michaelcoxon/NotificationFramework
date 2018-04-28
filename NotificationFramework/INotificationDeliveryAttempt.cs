using System;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface INotificationDeliveryAttempt
    {
        DateTimeOffset AttemptedDeliveryTime { get; }

        NotificationDeliveryResult Result { get; }

        string ResultMessage { get; }
    }
}