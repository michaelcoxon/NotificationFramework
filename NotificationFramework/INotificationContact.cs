namespace NotificationFramework
{
    public interface INotificationContact
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        string Identity { get; }
    }
}