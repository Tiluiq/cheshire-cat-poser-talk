using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockTextView : ITextView
    {
        public bool IsShowTextAsyncCalled { get; private set; }
        public bool IsHideTextAsyncCalled { get; private set; }

        public ValueTask ShowTextAsync(string targetId, string name, string text, CancellationToken cancellationToken = default)
        {
            IsShowTextAsyncCalled = true;
            return new ValueTask();
        }

        public ValueTask HideTextAsync(string targetId, CancellationToken cancellationToken = default)
        {
            IsHideTextAsyncCalled = true;
            return new ValueTask();
        }
    }
}
