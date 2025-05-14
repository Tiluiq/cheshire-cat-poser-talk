using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;
using R3;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockTextView : ITextView
    {
        public string TextId { get; }
        public string Text { get; private set; }
        public string Name { get; private set; }
        public bool IsShown { get; private set; }
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public MockTextView(string textId)
        {
            TextId = textId;
            onTextClicked.AddTo(disposables);
        }

        public Observable<Unit> OnTextClicked => onTextClicked;
        private readonly Subject<Unit> onTextClicked = new Subject<Unit>();
        public void SimulateTextClicked()
        {
            onTextClicked.OnNext(Unit.Default);
        }

        public ValueTask ShowTextAsync(string name, string text)
        {
            Name = name;
            Text = text;
            IsShown = true;
            return new ValueTask();
        }

        public ValueTask HideTextAsync()
        {
            IsShown = false;
            return new ValueTask();
        }

        public void Dispose()
        {
            disposables?.Dispose();
        }
    }
}
