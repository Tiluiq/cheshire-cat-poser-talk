using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario;

namespace CheshireCatPoserTalk.Application.MainGame
{
    public class ScenarioController
    {
        private readonly IAdvanceScenarioUseCase advanceScenarioUseCase;

        public ScenarioController(IAdvanceScenarioUseCase advanceScenarioUseCase)
        {
            this.advanceScenarioUseCase = advanceScenarioUseCase;
        }

        public async ValueTask PlayScenario(CancellationToken cancellationToken = default)
        {
            await advanceScenarioUseCase.AdvanceAsync(cancellationToken);
        }
    }
}
