using LookingForMyFriends.Domain;
using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Infrastructure.Services;
using LookingForMyFriends.Tests.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Xunit;
using Assert = Xunit.Assert;

namespace LookingForMyFriends.Tests.Infrastructure_Layer
{
    public class FriendServiceTests
    {
        private const string CacheKey = "friends";
        
        [Theory, AutoCheckOutData]
        public void Add_WithNameIsEmpty_ReturnUnsuccessful(FriendService sut, Friend newFriend)
        {
            newFriend.Name = string.Empty;

            var result = sut.Add(newFriend);

            Assert.False(result.Succeeded);
            Assert.True(result.Reason == ServiceResultFailReason.BusinessValidation);
            Assert.Contains("Informe o nome do seu amigo", result.Errors);
        }

        [Theory, AutoCheckOutData]
        public void Add_WithNameIsNull_ReturnUnsuccessful(FriendService sut, Friend newFriend)
        {
            newFriend.Name = null;

            var result = sut.Add(newFriend);

            Assert.False(result.Succeeded);
            Assert.True(result.Reason == ServiceResultFailReason.BusinessValidation);
            Assert.Contains("Informe o nome do seu amigo", result.Errors);
        }

        [Theory, AutoCheckOutData]
        public void Add_WithLocationIsNull_ReturnUnsuccessful(FriendService sut, Friend newFriend)
        {
            newFriend.Location = null;

            var result = sut.Add(newFriend);

            Assert.False(result.Succeeded);
            Assert.True(result.Reason == ServiceResultFailReason.BusinessValidation);
            Assert.Contains("Infome a localização do seu amigo.", result.Errors);
        }

        [Theory, AutoCheckOutData]
        public void Add_WithTheSameLocation_ReturnUnsuccessful(
            FriendService sut,
            Friend newFriend)
        {
            var myFriendsCollection = Builder<Friend>.CreateListOfSize(10).All()
                .TheFirst(1)
                .With(x => x.Name = "Sergio")
                .With(x => x.Location = new Location
                {
                    Latitude = 10, Longitude = 10
                })
                .Build()
                .ToList();

            sut.InMemoryCacheService.Remove(CacheKey);
            sut.InMemoryCacheService.Insert(CacheKey, myFriendsCollection);
            
            newFriend.Location.Latitude = 10;
            newFriend.Location.Longitude = 10;

            var result = sut.Add(newFriend);

            Assert.False(result.Succeeded);
            Assert.True(result.Reason == ServiceResultFailReason.BusinessValidation);
            Assert.Contains("Já existe um amigo nessa mesma localização.", result.Errors);
        }

        [Theory, AutoCheckOutData]
        public void Get_ReturnCollection(
            FriendService sut,
            List<Friend> friends)
        {
            sut.InMemoryCacheService.Insert(CacheKey, friends);
            
            var result = sut.Get();

            Assert.True(result.Succeeded);
            Assert.True(result.Object.Count == friends.Count);
        }

        [Theory, AutoCheckOutData]
        public void GetByName_ReturnFriendWithTheSameSame(
            FriendService sut)
        {
            var myFriendsCollection = Builder<Friend>.CreateListOfSize(10).All()
                .TheFirst(1)
                .With(x => x.Name = "Sergio")
                .TheNext(1)
                .With(x => x.Name = "Sergio")
                .Build()
                .ToList();

            sut.InMemoryCacheService.Remove(CacheKey);
            sut.InMemoryCacheService.Insert(CacheKey, myFriendsCollection);
            
            var result = sut.GetByName("Sergio");

            Assert.True(result.Succeeded);
            Assert.True(result.Object.Count == 2);
        }
    }
}
