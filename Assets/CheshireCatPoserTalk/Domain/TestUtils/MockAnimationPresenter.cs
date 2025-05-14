using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Presenters;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockAnimationPresenter : IAnimationPresenter
    {
        public bool IsPlayAnimaionEnded { get; private set; } = false;

        private float animationDuration;

        public MockAnimationPresenter(float animationDuration)
        {
            this.animationDuration = animationDuration;
        }

        public async ValueTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken)
        {
            // ダミーで待つ
            await Task.Delay((int)(animationDuration * 1000), cancellationToken: cancellationToken);
            IsPlayAnimaionEnded = true;
        }
    }
}
