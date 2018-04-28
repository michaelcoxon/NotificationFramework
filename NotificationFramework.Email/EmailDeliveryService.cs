using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework.Email
{
    public sealed class EmailDeliveryService : IEmailDeliveryService
    {
        private IEmailService _emailService;

        public EmailDeliveryService(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        public async Task<INotificationDeliveryAttempt> DeliverAsync(Notification notification, INotificationDelivery notificationDelivery)
        {
            var startDTO = DateTimeOffset.Now;
            var sender = CreateEmailNotificationContact(notification.Sender);
            var recipents = notificationDelivery.Recipients.Select(r => CreateEmailNotificationContact(r)).ToArray();

            using (var stream = new MemoryStream())
            using (var sw = new StreamWriter(stream))
            {
                await notification.Content.ExecuteAsync(sw);
                stream.Seek(0, SeekOrigin.Begin);

                var result = await this._emailService.DeliverAsync(sender, recipents, stream);

                return new EmailNotificationDeliveryAttempt
                {
                    AttemptedDeliveryTime = startDTO,
                    Result = result.NotificationDeliveryResult,
                    ResultMessage = result.ResultMessage,
                };
            }
        }

        private static EmailNotificationContact CreateEmailNotificationContact(INotificationContact contact)
        {
            return new EmailNotificationContact(contact.Identity, contact.DisplayName);
        }
    }
}
