using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Interfaces;
using CheshireCatPoserTalk.Entity.Scenario;

namespace CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario
{
    public class AdvanceScenarioUseCase : IAdvanceScenarioUseCase
    {
        private readonly IScenarioProvider scenarioProvider;
        private readonly ITextController textWindowController;
        private readonly IAnimationController animationController;
        private readonly IAwaiterService awaiterService;

        public AdvanceScenarioUseCase(IScenarioProvider scenarioProvider, ITextController textWindowController, IAnimationController animationController, IAwaiterService awaiterService)
        {
            this.scenarioProvider = scenarioProvider;
            this.textWindowController = textWindowController;
            this.animationController = animationController;
            this.awaiterService = awaiterService;
        }

        public async ValueTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            await foreach (var currentNode in scenarioProvider.GetScenarioNodesAsync(cancellationToken))
            {
                switch (currentNode)
                {
                    case TextNode textNode:
                        await textWindowController.ShowTextAsync(textNode.TargetId, textNode.Name, textNode.Text, cancellationToken);
                        if (textNode.WaitInput)
                        {
                            await awaiterService.WaitForInputAsync(cancellationToken);
                        }
                        break;

                    case HideTextNode hideTextNode:
                        await textWindowController.HideTextAsync(hideTextNode.TargetId, cancellationToken);
                        break;

                    case AnimationNode animationNode:
                        if (animationNode.WaitForCompletion)
                        {
                            await animationController.PlayAnimationAsync(animationNode.TargetId, animationNode.AnimationName, cancellationToken);
                        }
                        else
                        {
                            _ = animationController.PlayAnimationAsync(animationNode.TargetId, animationNode.AnimationName, cancellationToken);
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
