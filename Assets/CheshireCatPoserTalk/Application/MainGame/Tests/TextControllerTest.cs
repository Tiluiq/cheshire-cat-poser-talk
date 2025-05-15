using NUnit.Framework;
using CheshireCatPoserTalk.Application.TestUtils;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Application.MainGame.Tests
{
    public class TextControllerTest
    {
        private MockTextView textView;
        private TextController textController;

        [SetUp]
        public void Setup()
        {
            textView = new MockTextView();
            textController = new TextController(textView);
        }

        [Test]
        public async Task ShowTextAsync_ITextViewのShowTextAsyncが呼ばれる()
        {
            string targetId = "Target1";
            string name = "Name1";
            string text = "Text1";

            Assert.IsFalse(textView.IsShowTextAsyncCalled, "ITextViewのShowTextAsyncが呼ばれていないことを確認");

            await textController.ShowTextAsync(targetId, name, text);

            Assert.IsTrue(textView.IsShowTextAsyncCalled, "ITextViewのShowTextAsyncが呼ばれたことを確認");
        }

        [Test]
        public async Task HideTextAsync_ITextViewのHideTextAsyncが呼ばれる()
        {
            string targetId = "Target1";
            string name = "Name1";
            string text = "Text1";

            await textController.ShowTextAsync(targetId, name, text);

            Assert.IsFalse(textView.IsHideTextAsyncCalled, "ITextViewのHideTextAsyncが呼ばれていないことを確認");

            await textController.HideTextAsync(targetId);

            Assert.IsTrue(textView.IsHideTextAsyncCalled, "ITextViewのHideTextAsyncが呼ばれたことを確認");
        }
    }
}
