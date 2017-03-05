namespace ConnectMe.Api.Models.AccountResourceModels
{
    public class RegisterResourceModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MessagingToken { get; set; }
    }
}
