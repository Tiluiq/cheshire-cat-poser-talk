using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Entity.Scenario;

namespace CheshireCatPoserTalk.Domain.TestUtils
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

        public async IAsyncEnumerable<ScenarioNode> GetScenarioNodesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            for (var i = 0; i < scenarioNodes.Count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                await Task.Yield();
                yield return scenarioNodes[i];
                currentIndex++;
            }

            IsEnd = true;
        }
    }
}
