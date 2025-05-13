using NUnit.Framework;
using CheshireCatPoserTalk.Entity.GameStage;

namespace CheshireCatPoserTalk.Domain.UseCases.ChangeStage.Test
{
    public class ChangeStageUseCaseTest
    {
        private IChangeStageUseCase changeStageUseCase;
        private GameStage gameStage;

        [SetUp]
        public void Setup()
        {
            gameStage = new GameStage();
            changeStageUseCase = new ChangeStageUseCase(gameStage);
        }

        [Test]
        public void ChangeStage_ステージを変更することができる()
        {
            Assert.AreEqual(gameStage.CurrentStage, GameStageType.TitleStage);
            changeStageUseCase.ChangeStage(GameStageType.MainGameStage);
            Assert.AreEqual(gameStage.CurrentStage, GameStageType.MainGameStage);
            changeStageUseCase.ChangeStage(GameStageType.EndingStage);
            Assert.AreEqual(gameStage.CurrentStage, GameStageType.EndingStage);
            changeStageUseCase.ChangeStage(GameStageType.TitleStage);
            Assert.AreEqual(gameStage.CurrentStage, GameStageType.TitleStage);
        }

        [Test]
        public void OnStageChanged_ステージ変更時にイベントが発火する()
        {
            var stageChangedCount = 0;
            changeStageUseCase.OnStageChanged += (gameStageType) => { stageChangedCount++; };

            changeStageUseCase.ChangeStage(GameStageType.MainGameStage);
            Assert.AreEqual(1, stageChangedCount);
            changeStageUseCase.ChangeStage(GameStageType.EndingStage);
            Assert.AreEqual(2, stageChangedCount);
            changeStageUseCase.ChangeStage(GameStageType.TitleStage);
            Assert.AreEqual(3, stageChangedCount);
        }
    }
}
