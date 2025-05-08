using Entity.Scenario;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Domain.AdvanceScenario.Test
{
    class MockScenarioProvider : IScenarioProvider
    {
        public ScenarioNode Current { get; private set; }
        private List<ScenarioNode> nodes;
        private int currentIndex = -1;

        public async UniTask SetNodes(List<ScenarioNode> nodes)
        {
            this.nodes = nodes;
            await ResetAsync();
        }

        public UniTask ResetAsync(CancellationToken cancellationToken = default)
        {
            currentIndex = -1;
            Current = null;
            return UniTask.CompletedTask;
        }

        public UniTask<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            currentIndex++;
            if (currentIndex < nodes.Count)
            {
                Current = nodes[currentIndex];
                return UniTask.FromResult(true);
            }
            else
            {
                Current = null;
                return UniTask.FromResult(false);
            }
        }

        public bool IsEnd => currentIndex >= nodes.Count;

        public void Dispose() { }
    }
}
