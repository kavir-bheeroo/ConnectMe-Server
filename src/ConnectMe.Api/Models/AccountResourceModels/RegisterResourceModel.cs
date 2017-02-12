namespace ConnectMe.Api.Models.AccountResourceModels
{
    public class RegisterResourceModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MessagingToken { get; set; }
    }
}
