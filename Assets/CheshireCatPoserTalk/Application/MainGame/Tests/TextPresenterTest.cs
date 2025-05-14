using NUnit.Framework;
using CheshireCatPoserTalk.Application.TestUtils;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Application.MainGame.Tests
{
    public class TextPresenterTest
    {
        private MockTextViewFactory textViewFactory;
        private TextPresenter textPresenter;

        [SetUp]
        public void Setup()
        {
            textViewFactory = new MockTextViewFactory();
            textPresenter = new TextPresenter(textViewFactory);
        }

        [Test]
        public async Task ShowTextAsync_存在しないときに表示すると新しいTextViewを作成する()
        {
            string targetId = "Target1";
            string name = "Name1";
            string text = "Text1";

            Assert.AreEqual(0, textViewFactory.CreatedTextViews.Count);
            await textPresenter.ShowTextAsync(targetId, name, text);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);
            Assert.IsTrue(textViewFactory.CreatedTextViews[targetId].IsShown);
        }

        [Test]
        public async Task ShowTextAsync_存在するときに表示するとTextViewは更新される()
        {
            string targetId = "Target1";
            string name = "Name1";
            string text = "Text1";

            await textPresenter.ShowTextAsync(targetId, name, text);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);
            Assert.AreEqual(name, textViewFactory.CreatedTextViews[targetId].Name);
            Assert.AreEqual(text, textViewFactory.CreatedTextViews[targetId].Text);

            string newName = "Name2";
            string newText = "Text2";

            await textPresenter.ShowTextAsync(targetId, newName, newText);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);
            Assert.AreEqual(newName, textViewFactory.CreatedTextViews[targetId].Name);
            Assert.AreEqual(newText, textViewFactory.CreatedTextViews[targetId].Text);
        }

        [Test]
        public async Task ShowTextAsync_別のTextViewを表示すると新しいTextViewを作成する()
        {
            string targetId1 = "Target1";
            string name1 = "Name1";
            string text1 = "Text1";

            string targetId2 = "Target2";
            string name2 = "Name2";
            string text2 = "Text2";

            await textPresenter.ShowTextAsync(targetId1, name1, text1);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);

            await textPresenter.ShowTextAsync(targetId2, name2, text2);
            Assert.AreEqual(2, textViewFactory.CreatedTextViews.Count);
        }

        [Test]
        public async Task HideTextAsync_存在するときに非表示にしてもTextViewは削除しない()
        {
            string targetId = "Target1";
            string name = "Name1";
            string text = "Text1";

            await textPresenter.ShowTextAsync(targetId, name, text);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);

            await textPresenter.HideTextAsync(targetId);
            Assert.AreEqual(1, textViewFactory.CreatedTextViews.Count);
            Assert.IsFalse(textViewFactory.CreatedTextViews[targetId].IsShown);
        }

        [Test]
        public async Task HideTextAsync_存在しないときに非表示にしてもエラーは発生しない()
        {
            string targetId = "Target1";

            Assert.AreEqual(0, textViewFactory.CreatedTextViews.Count);
            await textPresenter.HideTextAsync(targetId);
            Assert.AreEqual(0, textViewFactory.CreatedTextViews.Count);
        }
    }
}
