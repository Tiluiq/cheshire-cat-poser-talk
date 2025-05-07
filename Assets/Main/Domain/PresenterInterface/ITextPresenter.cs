using System.Threading;
using Cysharp.Threading.Tasks;

namespace Domain.PresenterInterface
{
    public interface ITextPresenter
    {
        public UniTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default);
    }
}
