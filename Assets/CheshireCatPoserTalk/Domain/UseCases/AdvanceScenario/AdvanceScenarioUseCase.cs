using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Presenters;
using CheshireCatPoserTalk.Domain.Services;
using CheshireCatPoserTalk.Entity.Scenario;

namespace CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario
{
    public class AdvanceScenarioUseCase : IAdvanceScenarioUseCase
    {
        private readonly IScenarioProvider scenarioProvider;
        private readonly ITextPresenter textWindowPresenter;
        private readonly IAnimationPresenter animationPresenter;
        private readonly IAwaiterService awaiterService;

        public AdvanceScenarioUseCase(IScenarioProvider scenarioProvider, ITextPresenter textWindowPresenter, IAnimationPresenter animationPresenter, IAwaiterService awaiterService)
        {
            this.scenarioProvider = scenarioProvider;
            this.textWindowPresenter = textWindowPresenter;
            this.animationPresenter = animationPresenter;
            this.awaiterService = awaiterService;
        }

        public async ValueTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            await foreach (var currentNode in scenarioProvider.GetScenarioNodesAsync(cancellationToken))
            {
                switch (currentNode)
                {
                    case TextNode textNode:
                        await textWindowPresenter.ShowTextAsync(textNode.TargetId, textNode.Name, textNode.Text, cancellationToken);
                        if (textNode.WaitInput)
                        {
                            await awaiterService.WaitForInputAsync(cancellationToken);
                        }
                        break;

                    case HideTextNode hideTextNode:
                        await textWindowPresenter.HideTextAsync(hideTextNode.TargetId, cancellationToken);
                        break;

                    case AnimationNode animationNode:
                        if (animationNode.WaitForCompletion)
                        {
                            await animationPresenter.PlayAnimationAsync(animationNode.TargetId, animationNode.AnimationName, cancellationToken);
                        }
                        else
                        {
                            _ = animationPresenter.PlayAnimationAsync(animationNode.TargetId, animationNode.AnimationName, cancellationToken);
                        }
                        break;

                    case WaitNode waitNode:
                        await awaiterService.WaitSecondsAsync(waitNode.Duration, cancellationToken);
                        break;

                    default:
                        throw new System.NotImplementedException($"Node type {currentNode.GetType()} is not implemented.");
                }
            }
        }
    }
}
