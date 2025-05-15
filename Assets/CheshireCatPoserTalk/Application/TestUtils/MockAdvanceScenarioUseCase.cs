using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockAdvanceScenarioUseCase : IAdvanceScenarioUseCase
    {
        public bool IsAdvanceAsyncCalled { get; private set; }

        public ValueTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            IsAdvanceAsyncCalled = true;
            return new ValueTask();
        }
    }
}
