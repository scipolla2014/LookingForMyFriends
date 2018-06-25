using LookingForMyFriends.Domain;
using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Infrastructure.Services.Interfaces;
using LookingForMyFriends.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookingForMyFriends.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private const string CacheKey = "friends";

        public readonly IMemoryCacheService InMemoryCacheService;

        public FriendService(IMemoryCacheService inMemoryCacheService)
        {
            InMemoryCacheService = inMemoryCacheService ?? throw new ArgumentNullException(nameof(inMemoryCacheService));
        }

        public ServiceResult<Friend> Add(Friend friend)
        {
            var friends = GetCollectionInCache();

            var validator = new FriendValidator(friends);
            var results = validator.Validate(friend);

            if (results.IsValid == false)
            {
                return ServiceResult<Friend>.Fail(
                        ServiceResultFailReason.BusinessValidation,
                            results.Errors.Select(x => x.ErrorMessage).ToList());
            }

            AddFriendInCache(friend);

            return ServiceResult<Friend>.Success(friend);
        }

        public ServiceResult<List<Friend>> Get()
        {
            var friends = GetCollectionInCache();

            return ServiceResult<List<Friend>>.Success(friends);
        }

        public ServiceResult<List<Friend>> GetByName(string name)
        {
            var friends = GetCollectionInCache();
            return ServiceResult<List<Friend>>.Success(friends.Where(x => x.Name == name).ToList());
        }

        private List<Friend> GetCollectionInCache()
        {
            if (InMemoryCacheService.Exists(CacheKey) == false)
                InMemoryCacheService.Insert(CacheKey, new List<Friend>());
            
            return (List<Friend>)InMemoryCacheService.Get(CacheKey);
        }

        private void AddFriendInCache(Friend newFriend)
        {
            var friends = GetCollectionInCache();

            friends.Add(newFriend);

            InMemoryCacheService.Remove(CacheKey);

            InMemoryCacheService.Insert(CacheKey, friends);
        }
    }
}
