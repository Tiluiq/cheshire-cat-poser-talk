using System.Collections.Generic;
using NUnit.Framework;
using Entity.Scenario;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.PresenterInterface;
using Domain.AdvanceScenarioUseCase;
using System.Threading.Tasks;

public class AdvanceScenarioUseCaseTest
{
    class MockScenarioProvider : IScenarioProvider
    {
        public ScenarioNode Current { get; private set; }
        private List<ScenarioNode> nodes;
        private int currentIndex = 0;

        public MockScenarioProvider(List<ScenarioNode> nodes)
        {
            this.nodes = nodes;
            Current = nodes[currentIndex];
        }

        public UniTask ResetAsync(CancellationToken cancellationToken = default)
        {
            currentIndex = 0;
            Current = nodes[currentIndex];
            return UniTask.CompletedTask;
        }

        public UniTask<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            if (currentIndex < nodes.Count - 1)
            {
                currentIndex++;
                Current = nodes[currentIndex];
                return UniTask.FromResult(true);
            }
            return UniTask.FromResult(false);
        }

        public void Dispose() { }
    }

    class MockTextPresenter : ITextPresenter
    {
        public string LastName { get; private set; }
        public string LastText { get; private set; }

        public UniTask ShowTextAsync(string name, string text, CancellationToken cancellationToken = default)
        {
            LastName = name;
            LastText = text;
            return UniTask.CompletedTask;
        }
    }


    private MockScenarioProvider mockScenarioProvider;
    private MockTextPresenter mockTextPresenter;
    private AdvanceScenarioUseCase advanceScenarioUseCase;

    private List<ScenarioNode> scenarioNodes = new List<ScenarioNode>
    {
        new ScenarioNode(ActionType.Text, "Name1", "Hello"),
        new ScenarioNode(ActionType.None, "Name2", "World"),
        new ScenarioNode(ActionType.Text, "Name3", "AdvanceAsyncしてもこのノードには来ない"),
    };

    [SetUp]
    public void Setup()
    {
        mockScenarioProvider = new MockScenarioProvider(scenarioNodes);
        mockTextPresenter = new MockTextPresenter();
        advanceScenarioUseCase = new AdvanceScenarioUseCase(mockScenarioProvider, mockTextPresenter);
    }

    [Test]
    public void 初期状態()
    {
        Assert.AreEqual("Name1", mockScenarioProvider.Current.Name);
        Assert.AreEqual("Hello", mockScenarioProvider.Current.Text);
    }

    [Test]
    public async Task AdvanceAsync_テキストノードのとき_次のノードに進む()
    {
        await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);

        Assert.AreEqual("Name2", mockScenarioProvider.Current.Name);
        Assert.AreEqual("World", mockScenarioProvider.Current.Text);
        Assert.AreEqual("Name2", mockTextPresenter.LastName);
        Assert.AreEqual("World", mockTextPresenter.LastText);
    }

    [Test]
    public async Task AdvanceAsync_テキストノードでないとき_次のノードに進まない()
    {
        await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
        try
        {
            await advanceScenarioUseCase.AdvanceAsync(CancellationToken.None);
        }
        catch (System.InvalidOperationException ex)
        {
            Assert.AreEqual("Advanceできないノードです。", ex.Message);
        }
        Assert.AreEqual("Name2", mockScenarioProvider.Current.Name);
        Assert.AreEqual("World", mockScenarioProvider.Current.Text);
        Assert.AreEqual("Name2", mockTextPresenter.LastName);
        Assert.AreEqual("World", mockTextPresenter.LastText);
    }

}
