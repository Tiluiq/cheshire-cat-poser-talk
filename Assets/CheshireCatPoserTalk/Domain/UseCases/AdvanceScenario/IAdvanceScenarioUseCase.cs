using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario
{
    public interface IAdvanceScenarioUseCase
    {
        ValueTask AdvanceAsync(CancellationToken cancellationToken = default);
    }
}
