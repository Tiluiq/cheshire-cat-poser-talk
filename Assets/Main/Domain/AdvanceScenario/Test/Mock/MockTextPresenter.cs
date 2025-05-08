using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.PresenterInterface;

namespace Domain.AdvanceScenario.Test
{
    class MockTextPresenter : ITextWindowPresenter
    {
        public string LastName { get; private set; }
        public string LastText { get; private set; }

        public bool IsShowTextWindowCalled { get; private set; }
        public bool IsHideTextWindowCalled { get; private set; }

        public UniTask ShowTextWindowAsync(bool showName, CancellationToken cancellationToken = default)
        {
            IsShowTextWindowCalled = true;
            return UniTask.CompletedTask;
        }

        public UniTask HideTextWindowAsync(CancellationToken cancellationToken = default)
        {
            IsHideTextWindowCalled = true;
            return UniTask.CompletedTask;
        }

        public UniTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default)
        {
            LastName = name;
            LastText = text;
            return UniTask.CompletedTask;
        }
    }
}
