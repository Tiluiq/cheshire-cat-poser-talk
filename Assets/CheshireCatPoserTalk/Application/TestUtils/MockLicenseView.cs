using System;
using System.Collections.Generic;
using CheshireCatPoserTalk.Application.Title;
using R3;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockLicenseView : ILicenseView, IDisposable
    {
        public bool IsLicenseVisible { get; private set; }
        public List<string> Licenses { get; private set; }

        public void SetLicenses(List<string> licenses)
        {
            Licenses = licenses;
        }

        public void ShowLicense()
        {
            IsLicenseVisible = true;
        }

        public void HideLicense()
        {
            IsLicenseVisible = false;
        }

        public Observable<Unit> OnCloseButtonClicked => onLicenseCloseButtonClicked;
        private readonly Subject<Unit> onLicenseCloseButtonClicked = new Subject<Unit>();

        public void SimulateCloseButtonClick()
        {
            onLicenseCloseButtonClicked.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            onLicenseCloseButtonClicked.Dispose();
        }
    }
}
