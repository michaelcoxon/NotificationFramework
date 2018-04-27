namespace NotificationFramework.Email
{
    public sealed class EmailDeliveryResult
    {
        public NotificationDeliveryResult NotificationDeliveryResult { get; set; }
        public string ResultMessage { get; set; }
    }
}