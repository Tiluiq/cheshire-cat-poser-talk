using System.Threading;
using Cysharp.Threading.Tasks;

namespace Domain.Interface.Presenter
{
    public interface IAnimationPresenter
    {
        public UniTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken = default);
    }
}
