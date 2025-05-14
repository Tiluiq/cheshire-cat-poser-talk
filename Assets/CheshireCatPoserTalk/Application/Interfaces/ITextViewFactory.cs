namespace CheshireCatPoserTalk.Application.Interfaces
{
    public interface ITextViewFactory
    {
        ITextView CreateTextView(string targetId);
    }
}
