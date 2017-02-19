using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
