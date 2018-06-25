using LookingForMyFriends.Domain.Entities;
using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Main.Orchestration.Interface;
using System;
using System.Linq;

namespace LookingForMyFriends.Main.Services
{
    public class RegisterFriend : IOrchestrator
    {
        public readonly IFriendService FriendService;

        public RegisterFriend(IFriendService friendService)
        {
            FriendService = friendService ?? throw new ArgumentNullException(nameof(friendService));
        }

        public void Run()
        {
            string quantityOfFriends = null;
            var isValidNumber = false;

            while (isValidNumber == false)
                GetTotalFriends(out quantityOfFriends, out isValidNumber);

            var totalOfFriends = Convert.ToInt32(quantityOfFriends);

            var cont = 0;
            while (totalOfFriends > cont)
            {
                Console.WriteLine($"INFORME O NOME DO SEU AMIGO {cont + 1}:");
                var friend = new Friend { Name = Console.ReadLine() };

                var latitude = GetLocation("LATITUDE");
                friend.Location.Latitude = latitude;

                var longitude = GetLocation("LONGITUDE");
                friend.Location.Longitude = longitude;
                
                var serviceResult = FriendService.Add(friend);
                if (serviceResult.Succeeded == false)
                {
                    Console.WriteLine("ATENÇÃO: DADOS INFORMADOS ESTÃO INCORRETOS. " +
                                      $"ERROS: [{string.Join(", ", serviceResult.Errors.Select(e => e))}] \n");
                    Console.WriteLine("ATENÇÃO: POR FAVOR, INFORME NOVAMENTE \n");
                }
                else
                    cont++;

                Console.WriteLine("\n");
            }
        }

        private int GetLocation(string position)
        {
            int latitude;
            Console.WriteLine($"INFORME A LOCALIZAÇÃO DO SEU AMIGO ({position}):");

            while (int.TryParse(Console.ReadLine(), out latitude) == false)
                Console.WriteLine("ATENÇÃO: INFORME APENAS NÚMEROS. INFORME NOVAMENTE \n");

            return latitude;
        }

        private void GetTotalFriends(out string quantityOfFriends, out bool isValidNumber)
        {
            Console.WriteLine("INFORME A QUANTIDADE DE AMIGOS QUE VOCÊ POSSUI:");
            quantityOfFriends = Console.ReadLine();

            isValidNumber = IsValidNumber(quantityOfFriends);

            if (isValidNumber == false)
            {
                Console.WriteLine("ATENÇÃO: INFORME APENAS NÚMEROS, EX.: 10. VAMOS TENTAR NOVAMENTE...");
                return;
            }

            var totalFriends = Convert.ToInt32(quantityOfFriends);

            if (totalFriends <= 0)
            {
                Console.WriteLine("ATENÇÃO: INFORME UM VALOR POSITIVO MAIOR QUE 0");
                isValidNumber = false;
            }

        }

        private static bool IsValidNumber(string number)
        {
            return Int32.TryParse(number, out _);
        }
    }
}
