using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class EmailDeliveryService : INotificationDeliveryService<EmailNotificationDelivery>
    {
        private IEmailService _emailService;

        public EmailDeliveryService(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        public async Task<NotificationDeliveryAttempt> DeliverAsync(Notification notification, EmailNotificationDelivery notificationDelivery)
        {
            var sender = CreateEmailNotificationContact(notification.Sender);
            var recipents = notificationDelivery.Recipients.Select(r => CreateEmailNotificationContact(r)).ToArray();
            var ccRecipients = notificationDelivery.CarbonCopyRecipients.Select(r => CreateEmailNotificationContact(r)).ToArray();
            var bccRecipients = notificationDelivery.BlindCarbonCopyRecipients.Select(r => CreateEmailNotificationContact(r)).ToArray();
            var attachments = notification.Attachments.ToArray();

            if (!recipents.Any())
            {
                throw new NotSupportedException("Must have at least one recipient");
            }

            using (var stream = new MemoryStream())
            using (var sw = new StreamWriter(stream))
            {
                await notification.Content.ExecuteAsync(sw);
                await sw.FlushAsync();

                stream.Seek(0, SeekOrigin.Begin);

                var startDTO = DateTimeOffset.Now;

                var result = await this._emailService.DeliverAsync(sender, recipents, ccRecipients, bccRecipients, stream, attachments);

                return new NotificationDeliveryAttempt
                {
                    AttemptedDeliveryTime = startDTO,
                    Result = result.NotificationDeliveryResult,
                    ResultMessage = result.ResultMessage,
                };
            }
        }

        private static EmailNotificationContact CreateEmailNotificationContact(INotificationContact contact)
        {
            if (contact is EmailNotificationContact emailContact)
            {
                return emailContact;
            }

            throw new NotSupportedException($"Only {typeof(EmailNotificationContact)}'s are supported");
        }
    }
}
