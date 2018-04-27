using System;
using System.Collections.Generic;

namespace NotificationFramework
{
    public interface INotificationDelivery
    {
        ICollection<INotificationRecipient> Recipients { get; }
        DateTimeOffset ScheduledDeliveryTime { get; }

        DateTimeOffset ActualDeliveryTime { get; }

        int? MaxAttempts { get; } 

        IEnumerable<INotificationDeliveryAttempt> DeliveryAttempts { get; }
    }
}