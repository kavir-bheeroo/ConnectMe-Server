namespace ConnectMe.Api.Models.UserInfoResourceModels
{
    public class CreateUserInfoRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MessagingToken { get; set; }
        public string Image { get; set; }
    }
}
