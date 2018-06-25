using LookingForMyFriends.Domain.Interfaces;
using LookingForMyFriends.Infrastructure.Services;
using LookingForMyFriends.Infrastructure.Services.Interfaces;
using LookingForMyFriends.Main.Orchestration;
using LookingForMyFriends.Main.Orchestration.Interface;
using LookingForMyFriends.Main.Services;
using SimpleInjector;

namespace LookingForMyFriends.Main
{
    public static class DependencyInjection
    {
        public static void RegisterServices(Container container)
        {
            RegisterDomainServices(container);

            RegisterInfraServices(container);

            RegisterMainServices(container);

            container.Verify();
        }

        private static void RegisterMainServices(Container container)
        {
            container.Register<IOrchestrator, Orchestrator>(Lifestyle.Transient);

            container.RegisterCollection<IOrchestrator>(new[]
            {
                typeof(RegisterFriend),
                typeof(MyFriendsCollection),
                typeof(SearchClosestFriends)
            });
        }

        private static void RegisterDomainServices(Container container)
        {
            container.Register<IFriendService, FriendService>();
            container.Register<ISearchClosestFriendsService, SearchClosestFriendsService>();
        }

        private static void RegisterInfraServices(Container container)
        {
            container.Register<IMemoryCacheService, MemoryCacheService>();
        }
    }
}
