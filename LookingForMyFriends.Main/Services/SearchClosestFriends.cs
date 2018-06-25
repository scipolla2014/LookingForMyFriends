using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Main.Orchestration.Interface;
using System;
using System.Collections.Generic;

namespace LookingForMyFriends.Main.Services
{
    public class SearchClosestFriends : IOrchestrator
    {
        public readonly IFriendService FriendService;
        public readonly ISearchClosestFriendsService SearchClosestFriendsService;

        public SearchClosestFriends(
            IFriendService friendService,
            ISearchClosestFriendsService searchClosestFriendsService)
        {
            FriendService = friendService ?? throw new ArgumentNullException(nameof(friendService));
            SearchClosestFriendsService = searchClosestFriendsService ?? throw new ArgumentNullException(nameof(searchClosestFriendsService));
        }

        public void Run()
        {
            var found = false;

            List<Friend> friends = null;

            while (found == false)
                friends = GetMyFriendsByName(out found);

            foreach (var friend in friends)
            {
                Console.WriteLine($"AMIGO: {friend.Name} / LOCALIZAÇÃO: (LAT: {friend.Location.Latitude}) - (LONG: {friend.Location.Longitude})");
                Console.WriteLine("AMIGOS PRÓXIMOS:");

                var serviceResult = SearchClosestFriendsService.Find(friend, 3);

                if (serviceResult.Succeeded)
                {
                    var closeFriends = serviceResult.Object;

                    foreach (var closeFriend in closeFriends)
                        Console.WriteLine($"AMIGO: {closeFriend.Name} / LOCALIZAÇÃO: (LAT: {closeFriend.Location.Latitude}) - (LONG: {closeFriend.Location.Longitude})");
                }

                Console.WriteLine("\n");
            }
        }


        private List<Friend> GetMyFriendsByName(out bool found)
        {
            found = false;

            Console.WriteLine("INFORME O NOME DE SEU AMIGO:");
            var friendName = Console.ReadLine();

            var serviceResult = FriendService.GetByName(friendName);

            if (serviceResult.Succeeded)
            {
                var friends = serviceResult.Object;

                if (friends.Count == 0)
                    Console.WriteLine("ATENÇÃO: NÃO FOI ENCONTRADO NENHUM AMIGO COM ESSE NOME, FAVOR TENTAR NOVAMENTE...");
                else
                {
                    found = true;
                    return friends;
                }
            }

            return new List<Friend>();
        }
    }
}
