using System.Threading.Tasks;
using R3;

namespace CheshireCatPoserTalk.Application.Interfaces
{
    public interface ITextView
    {
        public string TextId { get; }
        public Observable<Unit> OnTextClicked { get; }
        public ValueTask ShowTextAsync(string name, string text);
        public ValueTask HideTextAsync();
        public void Dispose();
    }
}
