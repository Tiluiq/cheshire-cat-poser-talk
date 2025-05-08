using Entity.Scenario;
using Cysharp.Threading.Tasks;
using System.Threading;
using Domain.PresenterInterface;

namespace Domain.AdvanceScenario
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

        public async UniTask AdvanceAsync(CancellationToken cancellationToken = default)
        {
            var breakFlag = false;
            while (await scenarioProvider.MoveNextAsync(cancellationToken))
            {
                var currentNode = scenarioProvider.Current;

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
                        breakFlag = textNode.WaitInput;
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

                if (breakFlag)
                {
                    break;
                }
            }
        }
    }
}
