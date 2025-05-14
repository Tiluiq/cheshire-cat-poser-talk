using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.TestUtils;
using CheshireCatPoserTalk.Entity.Scenario;

namespace CheshireCatPoserTalk.Domain.UseCases.AdvanceScenario.Tests
{
    public class AdvanceScenarioUseCaseTest
    {
        private MockScenarioProvider mockScenarioProvider;
        private MockTextPresenter mockTextPresenter;
        private MockAnimationPresenter mockAnimationPresenter;
        private float animationDuration = 0.25f; // アニメーションのダミー時間
        private MockAwaiterService mockAwaiterService;
        private AdvanceScenarioUseCase advanceScenarioUseCase;

        [SetUp]
        public void SetUp()
        {
            mockScenarioProvider = new MockScenarioProvider();
            mockTextPresenter = new MockTextPresenter();
            mockAnimationPresenter = new MockAnimationPresenter(animationDuration);
            mockAwaiterService = new MockAwaiterService();
            advanceScenarioUseCase = new AdvanceScenarioUseCase(mockScenarioProvider, mockTextPresenter, mockAnimationPresenter, mockAwaiterService);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_空のとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>();
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_ShowTextWindowNodeのときShowTextWindowAsyncが呼ばれて終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new ShowTextWindowNode("Window1")
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockTextPresenter.IsShowTextWindowCalled);
            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_HideTextWindowNodeのときHideTextWindowAsyncが呼ばれて終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new HideTextWindowNode("Window1")
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockTextPresenter.IsHideTextWindowCalled);
            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_TextNodeのとき名前とテキストが設定される()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new TextNode("Name1", "Text1")
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            _ = advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
            await Task.Delay(100); // 少し待つ

            Assert.AreEqual("Name1", mockTextPresenter.LastName);
            Assert.AreEqual("Text1", mockTextPresenter.LastText);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_TextNodeのときインプットすると処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new TextNode("Name1", "Text1")
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            _ = advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
            await Task.Delay(100); // 少し待つ

            Assert.IsFalse(mockScenarioProvider.IsEnd); // 終了していない

            mockAwaiterService.ReceiveInput(); // インプットを完了させる
            await Task.Delay(100); // 少し待つ

            Assert.IsTrue(mockScenarioProvider.IsEnd); // 終了している
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_TextNodeでWaitInputがfalseのとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new TextNode("Name1", "Text1", false)
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test]
        public async Task AdvanceAsync_AnimationNodeでWaitForCompletionがfalseのとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new AnimationNode("Animation1")
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsFalse(mockAnimationPresenter.IsPlayAnimaionEnded); // アニメーションが終了していない

            await Task.Delay((int)(animationDuration * 1000 * 1.1)); // アニメーションのダミー時間＋α待つ

            Assert.IsTrue(mockAnimationPresenter.IsPlayAnimaionEnded); // アニメーションが終了している
            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test]
        public async Task AdvanceAsync_AnimationNodeでWaitForCompletionがtrueのとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new AnimationNode("Animation1", true)
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockAnimationPresenter.IsPlayAnimaionEnded);
            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test]
        public async Task AdvanceAsync_WaitNodeのとき処理が終了する()
        {
            var waitTime = 0.25f;
            var scenarioNodes = new List<ScenarioNode>
            {
                new WaitNode(waitTime)
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }

        [Test]
        public async Task AdvanceAsync_WaitNodeのとき指定時間待機する()
        {
            var waitTime = 0.25f;
            var scenarioNodes = new List<ScenarioNode>
            {
                new WaitNode(waitTime)
            };
            mockScenarioProvider.SetNodes(scenarioNodes);

            var startTime = System.DateTime.Now;
            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
            var endTime = System.DateTime.Now;

            var elapsedTime = (endTime - startTime).TotalSeconds;
            Assert.GreaterOrEqual(elapsedTime, waitTime);
        }
    }
}
