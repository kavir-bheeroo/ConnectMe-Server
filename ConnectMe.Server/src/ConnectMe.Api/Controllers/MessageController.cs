using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ConnectMe.Api.Models.MessageResourceModels;

namespace ConnectMe.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("send")]
        public void SendToCloud([FromBody]SendMessageRequest request)
        {

        }
    }
}
