using System.Collections.Generic;
using NUnit.Framework;
using Entity.Scenario;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.Interface.Presenter;
using System.Threading.Tasks;

namespace Domain.AdvanceScenario.Test

{
    public class AdvanceScenarioUseCaseTest
    {

        class MockTextPresenter : ITextWindowPresenter
        {
            public string LastName { get; private set; }
            public string LastText { get; private set; }

            public bool IsShowTextWindowCalled { get; private set; }
            public bool IsHideTextWindowCalled { get; private set; }

            public UniTask ShowTextWindowAsync(bool showName, CancellationToken cancellationToken = default)
            {
                IsShowTextWindowCalled = true;
                return UniTask.CompletedTask;
            }

            public UniTask HideTextWindowAsync(CancellationToken cancellationToken = default)
            {
                IsHideTextWindowCalled = true;
                return UniTask.CompletedTask;
            }

            public UniTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default)
            {
                LastName = name;
                LastText = text;
                return UniTask.CompletedTask;
            }
        }

        private MockScenarioProvider mockScenarioProvider;
        private MockTextPresenter mockTextPresenter;
        private MockAnimationPresenter mockAnimationPresenter;
        private float animationDuration = 0.25f; // アニメーションのダミー時間
        private AdvanceScenarioUseCase advanceScenarioUseCase;

        [SetUp]
        public void SetUp()
        {
            mockScenarioProvider = new MockScenarioProvider();
            mockTextPresenter = new MockTextPresenter();
            mockAnimationPresenter = new MockAnimationPresenter(animationDuration);
            advanceScenarioUseCase = new AdvanceScenarioUseCase(mockScenarioProvider, mockTextPresenter, mockAnimationPresenter);
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_空のとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>();
            await mockScenarioProvider.SetNodes(scenarioNodes);

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.AreEqual("Name1", mockTextPresenter.LastName);
            Assert.AreEqual("Text1", mockTextPresenter.LastText);
            Assert.IsFalse(mockScenarioProvider.IsEnd); // WaitInputがtrueなので終了しない
        }

        [Test, Timeout(1000)]
        public async Task AdvanceAsync_TextNodeでWaitInputがfalseのとき処理が終了する()
        {
            var scenarioNodes = new List<ScenarioNode>
            {
                new TextNode("Name1", "Text1", false)
            };
            await mockScenarioProvider.SetNodes(scenarioNodes);

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

            Assert.IsFalse(mockAnimationPresenter.IsPlayAnimaionEnded); // アニメーションが終了していない

            await UniTask.Delay((int)(animationDuration * 1000)); // アニメーションのダミー時間待つ

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

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
            await mockScenarioProvider.SetNodes(scenarioNodes);

            var startTime = System.DateTime.Now;
            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
            var endTime = System.DateTime.Now;

            var elapsedTime = (endTime - startTime).TotalSeconds;
            Assert.GreaterOrEqual(elapsedTime, waitTime);
            Assert.IsTrue(mockScenarioProvider.IsEnd);
        }
    }
}
