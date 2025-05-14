using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Presenters;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockTextPresenter : ITextWindowPresenter
    {
        public string LastName { get; private set; }
        public string LastText { get; private set; }

        public bool IsShowTextWindowCalled { get; private set; }
        public bool IsHideTextWindowCalled { get; private set; }

        public ValueTask ShowTextWindowAsync(bool showName, CancellationToken cancellationToken = default)
        {
            IsShowTextWindowCalled = true;
            return new ValueTask();
        }

        public ValueTask HideTextWindowAsync(CancellationToken cancellationToken = default)
        {
            IsHideTextWindowCalled = true;
            return new ValueTask();
        }

        public ValueTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default)
        {
            LastName = name;
            LastText = text;
            return new ValueTask();
        }
    }
}
