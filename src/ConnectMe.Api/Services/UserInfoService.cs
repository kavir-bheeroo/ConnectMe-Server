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

        public void AddWorker(CreateWorkerRequest request)
        {
            _databaseContext.Worker.Add(
                new Worker
                {
                    UserId = request.UserId,
                    WorkerTypeId = request.WorkerTypeId
                });
        }

        public async Task<FindUserResponse> FindNearbyUsers(FindUserRequest request)
        {
            // Get all users.
            var users = ((DbContext)_databaseContext).Set<UserInfo>();
            var distancesFromOrigin = new List<UserResourceModel>();

            // Calculate distance with all users. O(n)
            await users.ForEachAsync(u =>
                {
                    var user = _userManager.FindByIdAsync(u.UserId).Result;

                    distancesFromOrigin.Add(new UserResourceModel
                    {
                        Distance = CalculateDistance(request.Latitude, request.Longitude, u.Latitude, u.Longitude),
                        UserId = u.UserId,
                        Image = u.Image,
                        Latitude = u.Latitude,
                        Longitude = u.Longitude,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                });

            // Sort distance in ascending order. Use merge sort. O(n log n)
            // Take first n depending on number of records requested.
            var sortedUserListByDistances = distancesFromOrigin
                                            .OrderBy(u => u.Distance)
                                            .Take(request.NumberOfRecords ?? 10)
                                            .ToList();

            return new FindUserResponse
            {
                Users = sortedUserListByDistances
            };
        }

        public async Task<FindUserResponse> FindNearbyWorkers(FindUserRequest request)
        {
            // Get all users.
            var users = ((DbContext)_databaseContext).Set<UserInfo>();
            var distancesFromOrigin = new List<UserResourceModel>();

            // Calculate distance with all users. O(n)
            await users.ForEachAsync(async u =>
            {
                var user = await _userManager.FindByIdAsync(u.UserId);

                // Add only workers.
                if (user.IsWorker)
                {
                    var worker = _databaseContext.Worker.First(w => w.UserId.Equals(user.Id));

                    distancesFromOrigin.Add(new UserResourceModel
                    {
                        Distance = CalculateDistance(request.Latitude, request.Longitude, u.Latitude, u.Longitude),
                        UserId = u.UserId,
                        Image = u.Image,
                        Latitude = u.Latitude,
                        Longitude = u.Longitude,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        WorkerId = worker.Id,
                        WorkerTypeId = worker.WorkerTypeId
                    });
                }
            });

            // Sort distance in ascending order. Use merge sort. O(n log n)
            // Take first n depending on number of records requested.
            var sortedUserListByDistances = distancesFromOrigin
                                            .OrderBy(u => u.Distance)
                                            .Take(request.NumberOfRecords ?? 10)
                                            .ToList();

            return new FindUserResponse
            {
                Users = sortedUserListByDistances
            };
        }

        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Sqrt(Pow(x2 - x1, 2) + Pow(y2 - y1, 2));
        }
    }
}
