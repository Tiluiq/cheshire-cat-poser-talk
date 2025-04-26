using System;
using Entity.Scenario;
using Cysharp.Threading.Tasks;

namespace Domain.ExecuteScenarioUseCase
{
    public class ExecuteScenarioUseCase
    {
        private IScenarioProvider scenarioProvider;

        public ExecuteScenarioUseCase(IScenarioProvider scenarioProvider)
        {
            this.scenarioProvider = scenarioProvider;
        }

        public async UniTask LoadScenarioAsync()
        {
            await scenarioProvider.ResetAsync();
        }

        public async UniTask ExecuteScenarioAsync()
        {
            while (await scenarioProvider.MoveNextAsync())
            {
                var scenarioNode = scenarioProvider.Current;
                await ExecuteNode(scenarioNode);
            }
        }

        private UniTask ExecuteNode(ScenarioNode node)
        {
            Console.WriteLine($"Executing Node: {node.Name} - {node.Text}");
            return UniTask.CompletedTask;
        }
    }
}
