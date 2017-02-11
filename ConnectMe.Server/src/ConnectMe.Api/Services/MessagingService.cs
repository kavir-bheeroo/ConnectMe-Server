using ConnectMe.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public class MessagingService : IMessagingService
    {
        const string serverKey = "AAAAuQKPhKA:APA91bEzKWc78VPisSQa4BwCh9gCqJE8cOkdC_vC6HxzCDf8raYwA2_T95Ji1gzbleBBYmC4_Ma6rSYgNGfqL4dk0KeURYjnDST0jvP87JMg-9EQomeCvGzfy-y2XVuShU7sEL48WNpV";
        const string firebaseCMUrl = "https://fcm.googleapis.com/fcm/send";

        public async Task<HttpStatusCode> SendToFCM(string deviceToken, string title, string body)
        {
            try
            {
                var cloudMessage = new CloudMessage { To = deviceToken, Notification = new Notification { Title = title, Body = body } };
                var request = WebRequest.Create(firebaseCMUrl);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = $"key={serverKey}";

                var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                var serializedMessage = JsonConvert.SerializeObject(cloudMessage, jsonSerializerSettings);

                byte[] byteArray = Encoding.UTF8.GetBytes(serializedMessage);

                using (var dataStream = await request.GetRequestStreamAsync())
                {
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                var httpResponse = await request.GetResponseAsync() as HttpWebResponse;

                return httpResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
