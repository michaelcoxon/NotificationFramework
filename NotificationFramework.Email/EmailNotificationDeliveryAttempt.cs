using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework.Email
{
    public sealed class EmailNotificationDeliveryAttempt : INotificationDeliveryAttempt
    {

        public DateTimeOffset AttemptedDeliveryTime { get;  set; }

        public NotificationDeliveryResult Result { get;  set; }

        public string ResultMessage { get;  set; }
       
    }
}
