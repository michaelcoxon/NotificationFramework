namespace NotificationFramework
{
    public sealed class EmailNotificationContact : INotificationContact
    {
        public string DisplayName { get; }
        public string Identity { get; }
        public string EmailAddress => this.Identity;

        public EmailNotificationContact(string emailAddress, string displayName = default)
        {
            this.Identity = emailAddress;
            this.DisplayName = displayName;
        }
    }
}