using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Presenter;
using CheshireCatPoserTalk.Entity.Scenario;

namespace CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario
{
    public class AdvanceScenarioUseCase
    {
        private readonly IScenarioProvider scenarioProvider;
        private readonly ITextWindowPresenter textWindowPresenter;
        private readonly IAnimationPresenter animationPresenter;

        public AdvanceScenarioUseCase(IScenarioProvider scenarioProvider, ITextWindowPresenter textWindowPresenter, IAnimationPresenter animationPresenter)
        {
            this.scenarioProvider = scenarioProvider;
            this.textWindowPresenter = textWindowPresenter;
            this.animationPresenter = animationPresenter;
        }

        public async ValueTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            await foreach (var currentNode in scenarioProvider.GetScenarioNodesAsync(cancellationToken))
            {
                switch (currentNode)
                {
                    case ShowTextWindowNode showTextWindowNode:
                        await textWindowPresenter.ShowTextWindowAsync(showTextWindowNode.ShowName, cancellationToken);
                        break;

                    case HideTextWindowNode hideTextWindowNode:
                        await textWindowPresenter.HideTextWindowAsync(cancellationToken);
                        break;

                    case TextNode textNode:
                        await textWindowPresenter.ShowTextAsync(textNode.Name, textNode.Text, cancellationToken);
                        if (textNode.WaitInput)
                        {

                        }
                        break;

                    case AnimationNode animationNode:
                        if (animationNode.WaitForCompletion)
                        {
                            await animationPresenter.PlayAnimationAsync(animationNode.AnimationName, cancellationToken);
                        }
                        else
                        {
                            _ = animationPresenter.PlayAnimationAsync(animationNode.AnimationName, cancellationToken);
                        }
                        break;

                    case WaitNode waitNode:
                        await Task.Delay((int)(waitNode.Duration * 1000), cancellationToken: cancellationToken);
                        break;

                    default:
                        throw new System.NotImplementedException($"Node type {currentNode.GetType()} is not implemented.");
                }
            }
        }
    }
}
