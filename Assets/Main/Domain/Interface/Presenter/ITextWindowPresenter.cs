using System.Threading;
using Cysharp.Threading.Tasks;

namespace Domain.Interface.Presenter
{
    public interface ITextWindowPresenter
    {
        public UniTask ShowTextWindowAsync(bool showName, CancellationToken cancellationToken = default);
        public UniTask HideTextWindowAsync(CancellationToken cancellationToken = default);
        public UniTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default);
    }
}
