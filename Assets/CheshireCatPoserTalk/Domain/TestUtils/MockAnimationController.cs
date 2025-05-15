using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockAnimationController : IAnimationController
    {
        public bool IsPlayAnimaionEnded { get; private set; } = false;

        private float animationDuration;

        public MockAnimationController(float animationDuration)
        {
            this.animationDuration = animationDuration;
        }

        public async ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken)
        {
            // ダミーで待つ
            await Task.Delay((int)(animationDuration * 1000), cancellationToken: cancellationToken);
            IsPlayAnimaionEnded = true;
        }
    }
}
