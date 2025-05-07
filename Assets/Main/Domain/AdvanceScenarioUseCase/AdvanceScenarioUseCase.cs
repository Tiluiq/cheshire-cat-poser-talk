using Entity.Scenario;
using Cysharp.Threading.Tasks;
using System.Threading;
using Domain.PresenterInterface;

namespace Domain.AdvanceScenarioUseCase
{
    public class AdvanceScenarioUseCase
    {
        private readonly IScenarioProvider scenarioProvider;
        private readonly ITextPresenter textPresenter;

        public AdvanceScenarioUseCase(IScenarioProvider scenarioProvider, ITextPresenter textPresenter)
        {
            this.scenarioProvider = scenarioProvider;
            this.textPresenter = textPresenter;
        }

        public async UniTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            if (scenarioProvider.Current.Type == ActionType.Text)
            {
                await scenarioProvider.MoveNextAsync(cancellationToken);
                await textPresenter.ShowTextAsync(scenarioProvider.Current.Name, scenarioProvider.Current.Text, cancellationToken);
            }
            else
            {
                throw new System.InvalidOperationException("Advanceできないノードです。");
            }
        }
    }
}
