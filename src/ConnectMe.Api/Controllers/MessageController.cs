using ConnectMe.Api.Models;
using ConnectMe.Api.Models.MessageResourceModels;
using ConnectMe.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(IMessagingService messagingService, UserManager<ApplicationUser> userManager)
        {
            _messagingService = messagingService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("notify")]
        public async Task<IActionResult> SendNotificationMessageToCloud([FromBody]SendMessageRequest request)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var resultCode = await _messagingService.SendNotificationMessage(currentUser.Id, request.FromUserId, request.ReceiverToken, request.Title, request.Body);

            return resultCode == HttpStatusCode.OK ? new OkResult() : new StatusCodeResult((int)resultCode);
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendDataMessageToCloud([FromBody]SendMessageRequest request)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var resultCode = await _messagingService.SendDataMessage(currentUser.Id, request.FromUserId, request.ReceiverToken, request.Title, request.Body);

            return resultCode == HttpStatusCode.OK ? new OkResult() : new StatusCodeResult((int)resultCode);
        }
    }
}
