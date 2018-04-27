using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework.Email
{
    public interface IEmailDeliveryService
    {
        Task<EmailDeliveryResult> DeliverAsync(EmailNotificationContact sender, EmailNotificationContact[] recipients, Stream content);
        Task<EmailDeliveryResult> DeliverAsync(EmailNotificationContact sender, EmailNotificationContact[] recipients, Stream content, EmailNotificationAttachment[] attachments);
        Task<EmailDeliveryResult> DeliverAsync(EmailNotificationContact sender, EmailNotificationContact[] recipients, EmailNotificationContact[] ccRecipients, Stream content);
        Task<EmailDeliveryResult> DeliverAsync(EmailNotificationContact sender, EmailNotificationContact[] recipients, EmailNotificationContact[] ccRecipients, Stream content, EmailNotificationAttachment[] attachments);
    }
}
