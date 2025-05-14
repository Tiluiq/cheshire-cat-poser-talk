using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Presenters
{
    public interface IAnimationPresenter
    {
        public ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken = default);
    }
}
