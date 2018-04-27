using System;
using System.Collections.Generic;

namespace NotificationFramework
{
    public interface INotificationDelivery
    {
        ICollection<INotificationContact> Recipients { get; }

        DateTimeOffset ScheduledDeliveryTime { get; }

        DateTimeOffset? ActualDeliveryTime { get; }

        int? MaxAttempts { get; } 

        ICollection<INotificationDeliveryAttempt> DeliveryAttempts { get; }
    }
}