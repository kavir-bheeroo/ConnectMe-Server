namespace ConnectMe.Api.Models.UserInfoResourceModels
{
    public class UserResourceModel
    {
        public string UserId { get; set; }
        public string WorkerId { get; set; }
        public int WorkerTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MessagingToken { get; set; }
    }
}
