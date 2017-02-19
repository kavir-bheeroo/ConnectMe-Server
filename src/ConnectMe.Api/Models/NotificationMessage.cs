namespace ConnectMe.Api.Models
{
    public class NotificationMessage
    {
        public string To { get; set; }
        public Notification Notification { get; set; }
    }

    public class Notification
    {
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
