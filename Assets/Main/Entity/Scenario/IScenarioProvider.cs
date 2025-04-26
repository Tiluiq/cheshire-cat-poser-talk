using System.Threading;
using Cysharp.Threading.Tasks;

namespace Entity.Scenario
{
    public interface IScenarioProvider
    {
        UniTask ResetAsync(CancellationToken cancellationToken = default);
        UniTask<bool> MoveNextAsync(CancellationToken cancellationToken = default);
        ScenarioNode Current { get; }
        void Dispose();
    }
}