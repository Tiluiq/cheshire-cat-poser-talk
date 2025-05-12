using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Presenter
{
    public interface ITextWindowPresenter
    {
        public ValueTask ShowTextWindowAsync(bool showName, CancellationToken cancellationToken = default);
        public ValueTask HideTextWindowAsync(CancellationToken cancellationToken = default);
        public ValueTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default);
    }
}
