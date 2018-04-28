using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface IEmailService
    {
        Task<EmailDeliveryResult> DeliverAsync(EmailNotificationContact sender, EmailNotificationContact[] recipients, EmailNotificationContact[] carbonCopyRecipients, EmailNotificationContact[] blindCarbonCopyrecipients, Stream content, NotificationAttachment[] attachments);

    }
}
