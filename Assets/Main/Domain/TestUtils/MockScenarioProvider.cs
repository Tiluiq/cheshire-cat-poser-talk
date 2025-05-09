using Entity.Scenario;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Domain.TestUtils
{
    public class MockScenarioProvider : IScenarioProvider
    {
        private List<ScenarioNode> scenarioNodes;
        private int currentIndex;
        public bool IsEnd { get; private set; }

        public MockScenarioProvider()
        {
            scenarioNodes = new List<ScenarioNode>();
            currentIndex = 0;
            IsEnd = false;
        }

        public void SetNodes(List<ScenarioNode> nodes)
        {
            scenarioNodes = nodes;
            currentIndex = 0;
            IsEnd = false;
        }

        public IUniTaskAsyncEnumerable<ScenarioNode> GetScenarioNodesAsync(CancellationToken cancellationToken = default)
        {
            return UniTaskAsyncEnumerable.Create<ScenarioNode>(async (writer, token) =>
            {
                foreach (var node in scenarioNodes)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    await writer.YieldAsync(node);
                }

                IsEnd = !token.IsCancellationRequested;
            });
        }
    }
}
