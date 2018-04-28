using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NotificationFramework.UnitTests
{
    public class EmailDeliveryServiceTests
    {
        [Fact]
        public async Task DeliverSimpleEmail_Test()
        {
            var sender = new EmailNotificationContact("joe@example.com", "Joe Bloggs");
            var content = new StringNotificationContent("Hi Joe,\nYou have a thing due on 20/4/2018.");
            var notification = new Notification(sender, content);
            var notificationDelivery = new EmailNotificationDelivery(DateTimeOffset.Now, 3);

            notificationDelivery.Recipients.Add(new EmailNotificationContact("mary@example.com", "Mary Jones"));

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(m => m.DeliverAsync(It.IsAny<EmailNotificationContact>(), It.IsAny<EmailNotificationContact[]>(), It.IsAny<EmailNotificationContact[]>(), It.IsAny<EmailNotificationContact[]>(), It.IsAny<Stream>(), It.IsAny<NotificationAttachment[]>()))
                .Returns(() =>
                {
                    return Task.FromResult(new EmailDeliveryResult
                    {
                        NotificationDeliveryResult = NotificationDeliveryResult.Delivered,
                        ResultMessage = "OK"
                    });
                });

            var emailDeliveryService = new EmailDeliveryService(emailServiceMock.Object);

            var attempt = await emailDeliveryService.DeliverAsync(notification, notificationDelivery);
        }
    }
}
