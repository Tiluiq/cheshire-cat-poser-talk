using System;
using CheshireCatPoserTalk.Application.Title;
using R3;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockTitleView : ITitleView, IDisposable
    {
        public Observable<Unit> OnStartButtonClicked => onStartButtonClickedSubject;
        private readonly Subject<Unit> onStartButtonClickedSubject = new Subject<Unit>();

        public Observable<Unit> OnLicenseButtonClicked => onLicenseButtonClicked;
        private readonly Subject<Unit> onLicenseButtonClicked = new Subject<Unit>();

        public void SimulateStartButtonClick()
        {
            onStartButtonClickedSubject.OnNext(Unit.Default);
        }

        public void SimulateLicenseButtonClick()
        {
            onLicenseButtonClicked.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            onStartButtonClickedSubject.Dispose();
            onLicenseButtonClicked.Dispose();
        }
    }
}
