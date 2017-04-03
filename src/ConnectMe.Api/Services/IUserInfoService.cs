using ConnectMe.Api.Models.UserInfoResourceModels;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    public interface IUserInfoService
    {
        void AddUserInfo(CreateUserInfoRequest request, string userId);
        void UpdateUserInfo();
        void AddWorker(CreateWorkerRequest request);
        Task<FindUserResponse> FindNearbyUsers(FindUserRequest request);
        Task<FindUserResponse> FindNearbyWorkers(FindUserRequest request);
    }
}
