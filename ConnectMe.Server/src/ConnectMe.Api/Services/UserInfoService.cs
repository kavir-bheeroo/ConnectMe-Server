using ConnectMe.Api.Data;
using ConnectMe.Api.Models;
using ConnectMe.Api.Models.UserInfoResourceModels;
using System;

namespace ConnectMe.Api.Services
{
    public class UserInfoService : IUserInfoService
    {
        private IDatabaseContext _databaseContext;

        public UserInfoService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddUserInfo(CreateUserInfoRequest request, ApplicationUser user)
        {
            _databaseContext.UserInfo.Add(
                new UserInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    UserId = user.Id
                });

            _databaseContext.SaveChanges();
        }

        public void UpdateUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
