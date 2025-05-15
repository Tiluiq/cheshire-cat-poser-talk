using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.TestUtils;
using NUnit.Framework;

namespace CheshireCatPoserTalk.Application.MainGame.Tests
{
    public class ScenarioControllerTest
    {
        private MockAdvanceScenarioUseCase advanceScenarioUseCase;
        private ScenarioController scenarioController;

        [SetUp]
        public void Setup()
        {
            advanceScenarioUseCase = new MockAdvanceScenarioUseCase();
            scenarioController = new ScenarioController(advanceScenarioUseCase);
        }

        [Test]
        public async Task PlayScenario_AdvanceScenarioUseCaseのAdvanceAsyncが呼ばれる()
        {
            Assert.IsFalse(advanceScenarioUseCase.IsAdvanceAsyncCalled, "AdvanceScenarioUseCaseのAdvanceAsyncが呼ばれていないことを確認");

            await scenarioController.PlayScenario();

            Assert.IsTrue(advanceScenarioUseCase.IsAdvanceAsyncCalled, "AdvanceScenarioUseCaseのAdvanceAsyncが呼ばれたことを確認");
        }
    }
}
