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
        private readonly IEmailDeliveryService _emailDeliveryService;
        private readonly Notification _notification;

        public DateTimeOffset AttemptedDeliveryTime { get; private set; }

        public NotificationDeliveryResult Result { get; private set; }

        public string ResultMessage { get; private set; }

        public EmailNotificationDeliveryAttempt(IEmailDeliveryService emailDeliveryService, Notification notification)
        {
            this._emailDeliveryService = emailDeliveryService;
            this._notification = notification;
        }

        public async Task DeliverAsync()
        {
            this.AttemptedDeliveryTime = DateTimeOffset.Now;

            var sender = CreateEmailNotificationContact(this._notification.Sender);

            foreach (var delivery in this._notification.Deliveries.Where(d => !d.ActualDeliveryTime.HasValue))
            {
                var recipents = delivery.Recipients.Select(r => CreateEmailNotificationContact(r)).ToArray();

                using (var stream = new MemoryStream())
                using (var sw = new StreamWriter(stream))
                {
                    await this._notification.Content.ExecuteAsync(sw);
                    stream.Seek(0, SeekOrigin.Begin);

                    var result = await this._emailDeliveryService.DeliverAsync(sender, recipents, stream);

                    this.Result = result.NotificationDeliveryResult;
                    this.ResultMessage = result.ResultMessage;
                }
            }
        }

        private static EmailNotificationContact CreateEmailNotificationContact(INotificationContact contact)
        {
            return new EmailNotificationContact(contact.Identity, contact.DisplayName);
        }
    }
}
