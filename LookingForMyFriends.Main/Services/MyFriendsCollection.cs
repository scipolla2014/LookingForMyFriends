using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Main.Orchestration.Interface;
using System;

namespace LookingForMyFriends.Main.Services
{
    public class MyFriendsCollection : IOrchestrator
    {
        public readonly IFriendService FriendService;

        public MyFriendsCollection(IFriendService friendService)
        {
            FriendService = friendService ?? throw new ArgumentNullException(nameof(friendService));
        }

        public void Run()
        {
            Console.WriteLine("MEUS AMIGOS CADASTRADOS:");
            var serviceResult = FriendService.Get();

            if (serviceResult.Succeeded)
            {
                var friends = serviceResult.Object;

                foreach (var friend in friends)
                {
                    Console.WriteLine($"NOME: {friend.Name}");
                    Console.WriteLine($"LOCALIZAÇÃO: LATITUDE = {friend.Location.Latitude}");
                    Console.WriteLine($"LOCALIZAÇÃO: LONGITUDE = {friend.Location.Longitude}");

                    Console.WriteLine("\n");
                }
            }
        }
    }
}
