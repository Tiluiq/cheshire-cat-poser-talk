using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockTextPresenter : ITextPresenter
    {
        public string LastWindowId { get; private set; }
        public string LastName { get; private set; }
        public string LastText { get; private set; }

        public bool IsShowTextWindowCalled { get; private set; }
        public bool IsHideTextWindowCalled { get; private set; }

        public ValueTask ShowTextAsync(string windowId, string name, string text, CancellationToken cancellationToken = default)
        {
            LastName = name;
            LastText = text;
            return new ValueTask();
        }

        public ValueTask HideTextAsync(string windowId, CancellationToken cancellationToken = default)
        {
            IsHideTextWindowCalled = true;
            return new ValueTask();
        }
    }
}
