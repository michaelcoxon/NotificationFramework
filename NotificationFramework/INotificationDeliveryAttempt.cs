using System;

namespace NotificationFramework
{
    public interface INotificationDeliveryAttempt
    {
        DateTimeOffset AttemptedDeliveryTime { get; }
        NotificationDeliveryResult Result { get; }
        string ResultMessage { get; }
    }
}