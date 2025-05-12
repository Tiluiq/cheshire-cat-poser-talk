using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Presenter
{
    public interface IAnimationPresenter
    {
        public ValueTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken = default);
    }
}
