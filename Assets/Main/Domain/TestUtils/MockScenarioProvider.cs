using System.Collections.Generic;
using System.Threading;
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

        public IAsyncEnumerable<ScenarioNode> GetScenarioNodesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException("This method is not implemented yet.");
            // return UniTaskAsyncEnumerable.Create<ScenarioNode>(async (writer, token) =>
            // {
            //     foreach (var node in scenarioNodes)
            //     {
            //         if (token.IsCancellationRequested)
            //         {
            //             break;
            //         }

            //         await writer.YieldAsync(node);
            //     }

            //     IsEnd = !token.IsCancellationRequested;
            // });
        }
    }
}
