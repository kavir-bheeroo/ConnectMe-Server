namespace ConnectMe.Api.Models.MessageResourceModels
{
    public class SendMessageRequest
    {
        public string ReceiverToken { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
