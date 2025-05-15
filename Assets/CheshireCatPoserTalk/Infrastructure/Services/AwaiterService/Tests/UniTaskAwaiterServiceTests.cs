using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CheshireCatPoserTalk.Infrastructure.Services.AwaiterService.Tests
{
    public class UniTaskAwaiterServiceTests
    {
        private UniTaskAwaiterService uniTaskAwaiterService;

        [SetUp]
        public void Setup()
        {
            uniTaskAwaiterService = new UniTaskAwaiterService();
        }

        [Test]
        public async Task WaitSecondsAsync_指定した秒数待機する()
        {
            float seconds = 0.25f;

            var startTime = System.DateTime.Now;

            await uniTaskAwaiterService.WaitSecondsAsync(seconds);

            var endTime = System.DateTime.Now;
            var elapsedTime = (endTime - startTime).TotalSeconds;

            Assert.GreaterOrEqual(elapsedTime, seconds, "指定した秒数待機していることを確認する");
        }

        [Test]
        public async Task WaitSecondsAsync_0秒を指定した場合すぐに戻る()
        {
            float seconds = 0f;

            var startTime = System.DateTime.Now;

            await uniTaskAwaiterService.WaitSecondsAsync(seconds);

            var endTime = System.DateTime.Now;
            var elapsedTime = (endTime - startTime).TotalSeconds;

            Assert.Less(elapsedTime, 0.1f, "0秒を指定した場合、すぐに戻ることを確認する");
        }

        [Test]
        public async Task WaitSecondsAsync_0秒未満を指定した場合すぐに戻る()
        {
            float seconds = -0.1f;

            var startTime = System.DateTime.Now;

            await uniTaskAwaiterService.WaitSecondsAsync(seconds);

            var endTime = System.DateTime.Now;
            var elapsedTime = (endTime - startTime).TotalSeconds;

            Assert.Less(elapsedTime, 0.1f, "0秒未満を指定した場合、すぐに戻ることを確認する");
        }

        [Test]
        public async Task WaitSecondsAsync_キャンセルができる()
        {
            float seconds = 0.25f;
            var cts = new CancellationTokenSource();
            cts.CancelAfter(100); // 100ms後にキャンセル

            var startTime = System.DateTime.Now;

            try
            {
                await uniTaskAwaiterService.WaitSecondsAsync(seconds, cts.Token);
                Assert.Fail("タスクがキャンセルできていない");
            }
            catch (TaskCanceledException)
            {
                // 期待通りにキャンセルされている
            }

            var endTime = System.DateTime.Now;
            var elapsedTime = (endTime - startTime).TotalSeconds;

            Assert.Less(elapsedTime, seconds, "キャンセルトークンがキャンセルされた場合、すぐに戻ることを確認する");
        }

        [Test, Timeout(1000)]
        public async Task WaitForInputAsync_入力があるまで待機する()
        {
            var waitInputTask = uniTaskAwaiterService.WaitForInputAsync();

            await Task.Delay(100); // 少し待機してから入力を確認

            Assert.IsFalse(waitInputTask.IsCompleted, "入力がない状態で、タスクが完了していないことを確認する");

            uniTaskAwaiterService.OnInputReceived();

            await waitInputTask;

            Assert.IsTrue(waitInputTask.IsCompleted, "入力があった後、タスクが完了していることを確認する");
        }

        [Test, Timeout(1000)]
        public async Task WaitForInputAsync_キャンセルができる()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(100); // 100ms後にキャンセル

            try
            {
                await uniTaskAwaiterService.WaitForInputAsync(cts.Token);
                Assert.Fail("タスクがキャンセルできていない");
            }
            catch (TaskCanceledException)
            {
                Assert.Pass("期待通りにキャンセルされている");
            }
        }
    }
}
