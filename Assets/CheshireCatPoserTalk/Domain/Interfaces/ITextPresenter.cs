using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Interfaces
{
    public interface ITextPresenter
    {
        public ValueTask ShowTextAsync(string targetId, string name, string text, CancellationToken cancellationToken = default);
        public ValueTask HideTextAsync(string targetId, CancellationToken cancellationToken = default);
    }
}
