using System;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class NotificationDeliveryAttempt
    {
        public DateTimeOffset AttemptedDeliveryTime { get; set; }

        public NotificationDeliveryResult Result { get; set; }

        public string ResultMessage { get; set; }
    }
}