using System.Collections.Generic;
using CheshireCatPoserTalk.Application.Interfaces;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockTextViewFactory : ITextViewFactory
    {
        public Dictionary<string, MockTextView> CreatedTextViews { get; }

        public MockTextViewFactory()
        {
            CreatedTextViews = new Dictionary<string, MockTextView>();
        }

        public ITextView CreateTextView(string targetId)
        {
            var textView = new MockTextView(targetId);
            CreatedTextViews[targetId] = textView;
            return textView;
        }
    }
}
