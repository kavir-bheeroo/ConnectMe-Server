namespace ConnectMe.Api.Models.UserInfoResourceModels
{
    public class FindUserRequest
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int? NumberOfRecords { get; set; }
        public int? WorkerTypeId { get; set; }
    }
}
