using System.Collections.Generic;
using LookingForMyFriends.Domain.Entities;

namespace LookingForMyFriends.Domain.Interfaces
{
    public interface ISearchClosestFriendsService
    {
        ServiceResult<List<Friend>> Find(Friend friend, int take);
    }
}