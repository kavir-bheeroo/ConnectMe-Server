using ConnectMe.Api.Models.MessageResourceModels;
using ConnectMe.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ConnectMe.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessagingService _messagingService;

        public MessageController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        // POST api/values
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendToCloud([FromBody]SendMessageRequest request)
        {
            var resultCode = await _messagingService.SendToFCM(request.ReceiverToken, request.Title, request.Body);

            return resultCode == HttpStatusCode.OK ? new OkResult() : new StatusCodeResult((int)resultCode);
        }
    }
}
