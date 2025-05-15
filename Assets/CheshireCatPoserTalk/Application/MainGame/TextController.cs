using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Application.MainGame
{
    public class TextController : ITextController
    {
        private readonly ITextView textView;

        public TextController(ITextView textView)
        {
            this.textView = textView;
        }

        public async ValueTask ShowTextAsync(string targetId, string name, string text, CancellationToken cancellationToken = default)
        {
            await textView.ShowTextAsync(targetId, name, text, cancellationToken);
        }

        public async ValueTask HideTextAsync(string targetId, CancellationToken cancellationToken = default)
        {
            await textView.HideTextAsync(targetId, cancellationToken);
        }
    }
}
