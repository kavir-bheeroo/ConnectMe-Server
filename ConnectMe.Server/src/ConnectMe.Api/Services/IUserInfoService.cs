using ConnectMe.Api.Models;
using ConnectMe.Api.Models.UserInfoResourceModels;

namespace ConnectMe.Api.Services
{
    public interface IUserInfoService
    {
        void AddUserInfo(CreateUserInfoRequest request, ApplicationUser user);
        void UpdateUserInfo();
    }
}
