using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class StringNotificationContent : INotificationContent
    {
        private readonly string _notificationContent;

        public StringNotificationContent(string notificationContent)
        {
            this._notificationContent = notificationContent;
        }

        public async Task ExecuteAsync(TextWriter textWriter)
        {
            await textWriter.WriteAsync(this._notificationContent);
        }
    }
}
