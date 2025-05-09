using Entity.Scenario;
using Cysharp.Threading.Tasks;
using System.Threading;
using Domain.Interface.Presenter;

namespace Domain.AdvanceScenario
{
    public class AdvanceScenarioUseCase
    {
        private readonly IScenarioProvider scenarioProvider;
        private readonly ITextWindowPresenter textWindowPresenter;
        private readonly IAnimationPresenter animationPresenter;

        private UniTaskCompletionSource inputWaiter;

        public AdvanceScenarioUseCase(IScenarioProvider scenarioProvider, ITextWindowPresenter textWindowPresenter, IAnimationPresenter animationPresenter)
        {
            this.scenarioProvider = scenarioProvider;
            this.textWindowPresenter = textWindowPresenter;
            this.animationPresenter = animationPresenter;
        }

        public async UniTask AdvanceAsync(CancellationToken cancellationToken = default)
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
                            await WaitForInputAsync(cancellationToken);
                        }
                        break;

                    case AnimationNode animationNode:
                        if (animationNode.WaitForCompletion)
                        {
                            await animationPresenter.PlayAnimationAsync(animationNode.AnimationName, cancellationToken);
                        }
                        else
                        {
                            animationPresenter.PlayAnimationAsync(animationNode.AnimationName, cancellationToken).Forget();
                        }
                        break;

                    case WaitNode waitNode:
                        await UniTask.Delay((int)(waitNode.Duration * 1000), cancellationToken: cancellationToken);
                        break;

                    default:
                        throw new System.NotImplementedException($"Node type {currentNode.GetType()} is not implemented.");
                }
            }
        }

        private async UniTask WaitForInputAsync(CancellationToken cancellationToken)
        {
            inputWaiter = new UniTaskCompletionSource();
            await inputWaiter.Task.AttachExternalCancellation(cancellationToken);
        }

        public void CompleteInputWait()
        {
            inputWaiter?.TrySetResult();
            inputWaiter = null;
        }
    }
}
