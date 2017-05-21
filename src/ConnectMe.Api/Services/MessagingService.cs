using ConnectMe.Api.Data;
using ConnectMe.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public class MessagingService : IMessagingService
    {
        const string serverKey = "AAAAuQKPhKA:APA91bEzKWc78VPisSQa4BwCh9gCqJE8cOkdC_vC6HxzCDf8raYwA2_T95Ji1gzbleBBYmC4_Ma6rSYgNGfqL4dk0KeURYjnDST0jvP87JMg-9EQomeCvGzfy-y2XVuShU7sEL48WNpV";
        const string firebaseCMUrl = "https://fcm.googleapis.com/fcm/send";

        private readonly IDatabaseContext _databaseContext;

        public MessagingService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<HttpStatusCode> SendNotificationMessage(string currentUserId, string toUserId, string deviceToken, string title, string body)
        {
            try
            {
                var cloudMessage = new NotificationMessage { To = deviceToken, Notification = new Notification { Title = title, Body = body } };
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

                // Insert message in database.
                InsertMessage(new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    Body = body,
                    UserId = toUserId,
                    FromUserId = currentUserId,
                    IsRead = false,
                    Timestamp = DateTime.Now
                });

                return httpResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> SendDataMessage(string currentUserId, string toUserId, string deviceToken, string title, string body)
        {
            try
            {
                var cloudMessage = new DataMessage { To = deviceToken, Data = new Models.Data { Title = title, Body = body } };
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

                // Insert message in database.
                InsertMessage(new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    Body = body,
                    UserId = toUserId,
                    FromUserId = currentUserId,
                    IsRead = false,
                    Timestamp = DateTime.Now
                });

                return httpResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private void InsertMessage(Message message)
        {
            Task.Run(() =>
            {
                _databaseContext.Message.Add(message);
                _databaseContext.SaveChanges();
            });
        }
    }
}
