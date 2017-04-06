using ConnectMe.Api.Models;
using ConnectMe.Api.Models.UserInfoResourceModels;
using ConnectMe.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConnectMe.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public UserInfoController(
            IUserInfoService userInfoService,
            ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager)
        {
            _userInfoService = userInfoService;
            _logger = loggerFactory.CreateLogger<UserInfoController>();
            _userManager = userManager;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody]CreateUserInfoRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    _userInfoService.AddUserInfo(request, user.Id);
                    _logger.LogInformation(1, "User info created.");

                    return new ObjectResult("User info created.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return new ObjectResult("Error");
        }

        //todo: add a get for name/image/coordinates.. create resource model.

        [HttpPost]
        [Route("findNearbyUsers")]
        public async Task<IActionResult> FindNearbyUsers(FindUserRequest request)
        {
            request.NumberOfRecords = request.NumberOfRecords.HasValue ? request.NumberOfRecords : 10;
            return new OkObjectResult(await _userInfoService.FindNearbyUsers(request));
        }

        [HttpPost]
        [Route("findNearbyWorkers")]
        public async Task<IActionResult> FindNearbyWorkers(FindUserRequest request)
        {
            return new OkObjectResult(await _userInfoService.FindNearbyWorkers(request));
        }
    }
}
