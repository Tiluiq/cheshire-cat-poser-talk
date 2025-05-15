using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Application.MainGame
{
    public class AnimationController : IAnimationController
    {
        private readonly IAnimationView animationView;

        public AnimationController(IAnimationView animationView)
        {
            this.animationView = animationView;
        }

        public async ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken = default)
        {
            await animationView.PlayAnimationAsync(targetId, animationName, cancellationToken);
        }
    }
}
