using System.Collections.Generic;
using R3;

namespace CheshireCatPoserTalk.Application.Title
{
    public interface ILicenseView
    {
        void SetLicenses(List<string> licenses);
        void ShowLicense();
        void HideLicense();
        Observable<Unit> OnCloseButtonClicked { get; }
    }
}
