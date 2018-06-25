using LookingForMyFriends.Domain;
using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookingForMyFriends.Infrastructure.Services
{
    public class SearchClosestFriendsService : ISearchClosestFriendsService
    {
        public readonly IFriendService FriendService;

        public SearchClosestFriendsService(IFriendService friendService)
        {
            FriendService = friendService ?? throw new ArgumentNullException(nameof(friendService));
        }

        public ServiceResult<List<Friend>> Find(Friend myCurrentFriend, int take)
        {
            var myOthersFriends = GetOthersFriends(myCurrentFriend);

            var distanceBetweenFriends = CalculateDistanceBetweenFriends(myCurrentFriend, myOthersFriends);

            var closeFriends = distanceBetweenFriends
                                        .OrderBy(x => x.Value)
                                        .Take(take)
                                        .Select(friend => 
                                            myOthersFriends.First(x => x.Id == friend.Key))
                                .ToList();
            
            return ServiceResult<List<Friend>>.Success(closeFriends);
        }

        private Dictionary<Guid, double> CalculateDistanceBetweenFriends(
            Friend myCurrentFriend, 
            List<Friend> myOthersFriends)
        {
            var distanceResult = new Dictionary<Guid, double>();

            foreach (var friend in myOthersFriends)
            {
                var distance = Math.Sqrt(Math.Pow(myCurrentFriend.Location.Latitude - friend.Location.Latitude, 2)
                                         + Math.Pow(myCurrentFriend.Location.Longitude - friend.Location.Longitude, 2));
                distanceResult.Add(friend.Id, distance);
            }

            return distanceResult;
        }

        private List<Friend> GetOthersFriends(Friend myCurrentFriend)
        {
            var serviceResult = FriendService.Get();

            if (serviceResult.Succeeded)
            {
                var friends = serviceResult.Object
                    .Where(x => x.Location.Latitude != myCurrentFriend.Location.Latitude &&
                                x.Location.Longitude != myCurrentFriend.Location.Longitude);

                return friends.ToList();
            }

            return new List<Friend>();
        }
    }
}
