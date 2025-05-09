using System.Threading;
using Cysharp.Threading.Tasks;

namespace Entity.Scenario
{
    public interface IScenarioProvider
    {
        IUniTaskAsyncEnumerable<ScenarioNode> GetScenarioNodesAsync(CancellationToken cancellationToken = default);
    }
}