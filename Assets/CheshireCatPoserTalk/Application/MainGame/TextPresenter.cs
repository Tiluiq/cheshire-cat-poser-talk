using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Application.MainGame
{
    public class TextPresenter : ITextPresenter
    {
        private readonly ITextViewFactory TextViewFactory;
        private readonly Dictionary<string, ITextView> shownTextViews;

        public TextPresenter(ITextViewFactory textViewFactory)
        {
            TextViewFactory = textViewFactory;
            shownTextViews = new Dictionary<string, ITextView>();
        }

        public async ValueTask ShowTextAsync(string targetId, string name, string text, CancellationToken cancellationToken = default)
        {
            if (shownTextViews.TryGetValue(targetId, out var textView))
            {
                await textView.ShowTextAsync(name, text);
            }
            else
            {
                var newTextView = TextViewFactory.CreateTextView(targetId);
                await newTextView.ShowTextAsync(name, text);
                shownTextViews[targetId] = newTextView;
            }
        }

        public async ValueTask HideTextAsync(string targetId, CancellationToken cancellationToken = default)
        {
            if (shownTextViews.TryGetValue(targetId, out var textView))
            {
                await textView.HideTextAsync();
            }
        }
    }
}
