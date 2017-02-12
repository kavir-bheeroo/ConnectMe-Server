using System.Net;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public interface IMessagingService
    {
        Task<HttpStatusCode> SendToFCM(string deviceToken, string title, string body);
    }
}
