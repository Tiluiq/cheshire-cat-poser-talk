using System;
using CheshireCatPoserTalk.Domain.UseCases.ChangeStage;
using CheshireCatPoserTalk.Domain.UseCases.GetLicense;
using CheshireCatPoserTalk.Entity.GameStage;
using R3;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace CheshireCatPoserTalk.Application.Title
{
    public class TitlePresenter : IAsyncStartable, IDisposable
    {
        private readonly ITitleView titleView;
        private readonly IChangeStageUseCase changeStageUseCase;


        private readonly ILicenseView licenseView;
        private readonly IGetLicenseUseCase getLicenseUseCase;

        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public TitlePresenter(ITitleView titleView, IChangeStageUseCase changeStageUseCase, ILicenseView licenseView, IGetLicenseUseCase getLicenseUseCase)
        {
            this.titleView = titleView;
            this.titleView.OnStartButtonClicked.Subscribe(_ => OnStartButtonClicked()).AddTo(disposables);
            this.titleView.OnLicenseButtonClicked.Subscribe(_ => OnLicenseButtonClicked()).AddTo(disposables);

            this.changeStageUseCase = changeStageUseCase;

            this.licenseView = licenseView;
            this.licenseView.OnCloseButtonClicked.Subscribe(_ => OnLicenseCloseButtonClicked()).AddTo(disposables);

            this.getLicenseUseCase = getLicenseUseCase;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {

            var licenses = await getLicenseUseCase.GetLicensesAsync();
            licenseView.SetLicenses(licenses);
        }

        private void OnStartButtonClicked()
        {
            changeStageUseCase.ChangeStage(GameStageType.MainGameStage);
        }

        private void OnLicenseButtonClicked()
        {
            licenseView.ShowLicense();
        }

        private void OnLicenseCloseButtonClicked()
        {
            licenseView.HideLicense();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
