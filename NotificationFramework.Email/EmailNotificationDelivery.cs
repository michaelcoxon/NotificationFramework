using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationFramework
{
    public class EmailNotificationDelivery : INotificationDelivery
    {
        public ICollection<EmailNotificationContact> Recipients { get; }

        public ICollection<EmailNotificationContact> CarbonCopyRecipients { get; }

        public ICollection<EmailNotificationContact> BlindCarbonCopyRecipients { get; }

        public DateTimeOffset ScheduledDeliveryTime { get; }

        public DateTimeOffset? ActualDeliveryTime { get; set; }

        public DateTimeOffset? ViewedTime { get; set; }

        public DateTimeOffset? Dismissed { get; set; }

        public int? MaxAttempts { get; }

        public ICollection<NotificationDeliveryAttempt> DeliveryAttempts { get; }


        public EmailNotificationDelivery(DateTimeOffset scheduledDeliveryTime, int? MaxAttempts = null)
        {
            this.Recipients = new List<EmailNotificationContact>();
            this.CarbonCopyRecipients = new List<EmailNotificationContact>();
            this.BlindCarbonCopyRecipients = new List<EmailNotificationContact>();
            this.DeliveryAttempts = new List<NotificationDeliveryAttempt>();

            this.ScheduledDeliveryTime = scheduledDeliveryTime;
            this.MaxAttempts = MaxAttempts;
        }

        ICollection<INotificationContact> INotificationDelivery.Recipients => this.Recipients.Cast<INotificationContact>().ToList();
    }
}