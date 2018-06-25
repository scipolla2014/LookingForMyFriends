using AutoFixture;
using AutoFixture.Kernel;
using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Infrastructure.Services;
using LookingForMyFriends.Infrastructure.Services.Interfaces;
using System.Collections.Generic;

namespace LookingForMyFriends.Tests.AutoFixture
{
    internal class AutoCheckOutDataCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IMemoryCacheService),
                    typeof(MemoryCacheService)));

            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IFriendService),
                    typeof(FriendService)));

            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ISearchClosestFriendsService),
                    typeof(SearchClosestFriendsService)));

            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(object),
                    typeof(List<Friend>)));
        }
    }
}