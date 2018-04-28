using System;
using System.Collections.Generic;

namespace NotificationFramework
{
    public interface INotificationDelivery
    {
        ICollection<INotificationContact> Recipients { get; }

        DateTimeOffset ScheduledDeliveryTime { get; }

        DateTimeOffset? ActualDeliveryTime { get; }

        DateTimeOffset? ViewedTime { get; }

        DateTimeOffset? Dismissed { get; }

        int? MaxAttempts { get; } 

        ICollection<NotificationDeliveryAttempt> DeliveryAttempts { get; }
    }
}