using ConnectMe.Api.Models;

namespace ConnectMe.Api.Services
{
    public interface IUserInfoService
    {
        void AddUserInfo(CreateUserInfoRequest request, ApplicationUser user);
        void UpdateUserInfo();
    }
}
