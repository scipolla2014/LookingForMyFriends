using LookingForMyFriends.Domain.Entities;
using System.Collections.Generic;

namespace LookingForMyFriends.Domain.Interfaces
{
    public interface IFriendService
    {
        ServiceResult<Friend> Add(Friend friend);

        ServiceResult<List<Friend>> Get();

        ServiceResult<List<Friend>> GetByName(string name);
    }
}
