using System.Collections.Generic;
using System.Threading;

namespace CheshireCatPoserTalk.Entity.Scenario
{
    public interface IScenarioProvider
    {
        IAsyncEnumerable<ScenarioNode> GetScenarioNodesAsync(CancellationToken cancellationToken = default);
    }
}