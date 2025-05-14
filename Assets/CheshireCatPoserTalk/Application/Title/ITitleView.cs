using R3;

namespace CheshireCatPoserTalk.Application.Title
{
    public interface ITitleView
    {
        Observable<Unit> OnStartButtonClicked { get; }
        Observable<Unit> OnLicenseButtonClicked { get; }
    }
}
