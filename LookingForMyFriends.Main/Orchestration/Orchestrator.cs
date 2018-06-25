using LookingForMyFriends.Main.Orchestration.Interface;
using System;
using System.Collections.Generic;

namespace LookingForMyFriends.Main.Orchestration
{
    public class Orchestrator : IOrchestrator
    {
        public readonly IEnumerable<IOrchestrator> Operations;

        public Orchestrator(params IOrchestrator[] operations)
        {
            Operations = operations ?? throw new ArgumentNullException(nameof(operations));
        }

        public void Run()
        {
            foreach (var operation in Operations)
                operation.Run();
        }
    }
}
