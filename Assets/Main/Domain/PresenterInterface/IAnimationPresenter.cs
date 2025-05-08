using System.Threading;
using Cysharp.Threading.Tasks;

namespace Domain.PresenterInterface
{
    public interface IAnimationPresenter
    {
        public UniTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken = default);
    }
}
