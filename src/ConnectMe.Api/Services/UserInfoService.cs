using ConnectMe.Api.Data;
using ConnectMe.Api.Models;
using ConnectMe.Api.Models.UserInfoResourceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;

namespace ConnectMe.Api.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInfoService(IDatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        public void AddUserInfo(CreateUserInfoRequest request, string userId)
        {
            _databaseContext.UserInfo.Add(
                new UserInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    UserId = userId,
                    MessagingToken = request.MessagingToken,
                    Image = request.Image
                });

            _databaseContext.SaveChanges();
        }

        public void UpdateUserInfo()
        {
            throw new NotImplementedException();
        }

        public async Task<FindUserResponse> FindNearbyUsers(FindUserRequest request)
        {
            // Get all users.
            var users = ((DbContext)_databaseContext).Set<UserInfo>();
            var distancesFromOrigin = new List<double>();

            // Calculate distance with all users. O(n)
            await users.ForEachAsync((u) => 
                {
                    distancesFromOrigin.Add(CalculateDistance(request.Latitude, request.Longitude, u.Latitude, u.Longitude));
                });

            // Sort distance in ascending order. Use merge sort. O(n log n)
            distancesFromOrigin.Sort();

            // Take first n depending on number of records requested.
            //return distancesFromOrigin.Take(request.NumberOfRecords.Value);

            return null;
        }

        public FindUserResponse FindNearbyWorkers(FindUserRequest request)
        {
            throw new NotImplementedException();
        }

        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Sqrt(Pow(x2 - x1, 2) + Pow(y2 - y1, 2));
        }
    }
}
