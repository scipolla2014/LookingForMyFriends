using LookingForMyFriends.Main.Orchestration.Interface;
using SimpleInjector;
using System;

namespace LookingForMyFriends.Main
{
    static class Program
    {
        private static readonly IOrchestrator Orchestrator;

        static Program()
        {
            var container = new Container();
            DependencyInjection.RegisterServices(container);
            Orchestrator = container.GetInstance<IOrchestrator>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("[LOOKING FOR MY FRIENDS] STARTED. \n");
            Console.WriteLine("###### Informe os dados a seguir ######");

            try
            {
                Orchestrator.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro inesperado na aplicação.");
                Console.Write("Pressione alguma tecla para sair...");
            }
            
            Console.Write("Pressione alguma tecla para sair...");
            Console.ReadKey();
        }
    }
}
