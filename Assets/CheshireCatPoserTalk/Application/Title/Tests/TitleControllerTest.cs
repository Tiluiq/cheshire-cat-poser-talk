using NUnit.Framework;
using CheshireCatPoserTalk.Entity.GameStage;
using CheshireCatPoserTalk.Domain.UseCases.ChangeStage;
using CheshireCatPoserTalk.Domain.UseCases.GetLicense;
using CheshireCatPoserTalk.Application.TestUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Application.Title.Tests
{
    public class TitleControllerTest
    {
        private TitleController titlePresenter;
        private MockTitleView titleView;
        private IChangeStageUseCase changeStageUseCase;
        private MockLicenseView licenseView;
        private IGetLicenseUseCase getLicenseUseCase;

        private List<string> licenses = new List<string>
        {
            "License1\n" +
            "This is a test license 1.\n",
            "License2\n" +
            "This is a test license 2.\n",
            "License3\n" +
            "This is a test license 3.\n",
        };

        [SetUp]
        public void Setup()
        {
            titleView = new MockTitleView();
            changeStageUseCase = new MockChangeStageUseCase();
            licenseView = new MockLicenseView();
            getLicenseUseCase = new MockGetLicenseUseCase(licenses);
            titlePresenter = new TitleController(titleView, changeStageUseCase, licenseView, getLicenseUseCase);
        }

        [Test]
        public void OnStartButtonClicked_スタートボタンが押されたときMainGameStageに遷移する()
        {
            Assert.AreEqual(GameStageType.TitleStage, changeStageUseCase.CurrentStage);
            titleView.SimulateStartButtonClick();
            Assert.AreEqual(GameStageType.MainGameStage, changeStageUseCase.CurrentStage);
        }

        [Test]
        public void OnLicenseButtonClicked_ライセンスボタンが押されたときライセンス画面を表示する()
        {
            Assert.IsFalse(licenseView.IsLicenseVisible);
            titleView.SimulateLicenseButtonClick();
            Assert.IsTrue(licenseView.IsLicenseVisible);
        }

        [Test]
        public void OnLicenseCloseButtonClicked_ライセンス画面を閉じる()
        {
            titleView.SimulateLicenseButtonClick();
            Assert.IsTrue(licenseView.IsLicenseVisible);
            licenseView.SimulateCloseButtonClick();
            Assert.IsFalse(licenseView.IsLicenseVisible);
        }

        [Test]
        public async Task SetLicenses_ライセンスが設定される()
        {
            Assert.IsNull(licenseView.Licenses);
            await titlePresenter.SetLicensesAsync();
            Assert.IsNotNull(licenseView.Licenses);
        }
    }
}
