using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        public NotificationController()
        {
            
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                var url = "https://fcm.googleapis.com/fcm/send";
                var request = WebRequest.Create(url);
                string httpResponse = null;

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "key=AAAAP06ZAYw:APA91bGS66L-vzpsWhMoptyBciPuI6gjoQxrx4NbQjjb2zmhdceB6rs6JdrcEKo0j3jeoeD9PlnL4FrM0607o7SZ7HHxUG4Axd0N8_Ki1UekCxZHlOyuIjS4sKxr8LfILCP2OZ71pbR_Xfd_MV4r9vWKM8EDQUjtbg";
                
                string postData = "{ \"data\": {\"score\": \"5x1\",},\"to\" : \"bk3RNwTe3H0:CI2k_HHwgIpoDKCIZvvDMExUdFQ3P1...\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes (postData);

                using (var dataStream = await request.GetRequestStreamAsync())
                {
                    // Write the data to the request stream.
                    dataStream.Write (byteArray, 0, byteArray.Length);
                }

                var response = await request.GetResponseAsync();

                using (var responseStream = response.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream))
                {
                    httpResponse = streamReader.ReadToEnd();
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}