﻿using ConnectMe.Api.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserInfoController(
            IUserInfoService userInfoService,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory)
        {
            _userInfoService = userInfoService;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<UserInfoController>();
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

                    _userInfoService.AddUserInfo(request, user);
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
    }
}
