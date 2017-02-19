using System.Net;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public interface IMessagingService
    {
        Task<HttpStatusCode> SendNotificationMessage(string deviceToken, string title, string body);
        Task<HttpStatusCode> SendDataMessage(string deviceToken, string title, string body);
    }
}
