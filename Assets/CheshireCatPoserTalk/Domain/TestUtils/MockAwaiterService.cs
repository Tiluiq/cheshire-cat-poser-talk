using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Interfaces;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockAwaiterService : IAwaiterService
    {
        public ValueTask WaitSecondsAsync(float seconds, CancellationToken cancellationToken = default)
        {
            return new ValueTask(Task.Delay((int)(seconds * 1000), cancellationToken));
        }

        private TaskCompletionSource<bool> taskCompletionSource;

        public ValueTask WaitForInputAsync(CancellationToken cancellationToken = default)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            return new ValueTask(taskCompletionSource.Task);
        }

        public void ReceiveInput()
        {
            taskCompletionSource?.TrySetResult(true);
            taskCompletionSource = null;
        }
    }
}
