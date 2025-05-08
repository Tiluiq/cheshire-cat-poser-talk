using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.PresenterInterface;

namespace Domain.AdvanceScenario.Test
{
    public class MockAnimationPresenter : IAnimationPresenter
    {
        public bool IsPlayAnimaionEnded { get; private set; } = false;

        private float animationDuration;

        public MockAnimationPresenter(float animationDuration)
        {
            this.animationDuration = animationDuration;
        }

        public async UniTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken)
        {
            // ダミーで待つ
            await UniTask.Delay((int)(animationDuration * 1000), cancellationToken: cancellationToken);
            IsPlayAnimaionEnded = true;
        }
    }
}
