namespace NotificationFramework
{
    public interface INotificationRecipient
    {
        string DisplayName { get; }
        string Identity { get; }
    }
}