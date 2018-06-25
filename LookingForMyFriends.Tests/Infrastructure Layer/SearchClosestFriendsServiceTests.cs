using AutoFixture.Idioms;
using FizzWare.NBuilder;
using LookingForMyFriends.Domain;
using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Infrastructure.Services;
using LookingForMyFriends.Tests.AutoFixture;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LookingForMyFriends.Tests.Infrastructure_Layer
{
    public class SearchClosestFriendsServiceTests
    {
        [Theory, AutoCheckOutData]
        public void SearchClosestFriendsService_ShouldGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(SearchClosestFriendsService).GetConstructors());
        }

        [Theory, AutoCheckOutData]
        public void FindClosestFriend_ShouldBeReturnThreeFriends()
        {
            var myFriendsCollection = Builder<Friend>.CreateListOfSize(5).All()
                                .TheFirst(1)
                                    .With(x => x.Name = "Sergio")
                                    .With(x => x.Location = new Location
                                    {
                                        Latitude = 10,
                                        Longitude = 20
                                    })
                           .Build()
                           .ToList();

            var friendServiceMock = Substitute.For<IFriendService>();
            friendServiceMock.Get().Returns(ServiceResult<List<Friend>>.Success(myFriendsCollection));

            var sut = new SearchClosestFriendsService(friendServiceMock);

            var currentFriend = myFriendsCollection[0];

            var result = sut.Find(currentFriend, 3);

            Assert.True(result.Succeeded);
            Assert.True(result.Object.Count == 3);
        }
    }
}