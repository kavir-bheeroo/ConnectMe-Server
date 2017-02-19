namespace ConnectMe.Api.Models
{
    public class DataMessage
    {
        public string To { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
