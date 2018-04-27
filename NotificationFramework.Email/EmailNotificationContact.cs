namespace NotificationFramework.Email
{
    public sealed class EmailNotificationContact
    {
        public string DisplayName { get; }
        public string EmailAddress { get; }

        public EmailNotificationContact(string emailAddress, string displayName = default)
        {
            this.EmailAddress = emailAddress;
            this.DisplayName = displayName;
        }
    }
}