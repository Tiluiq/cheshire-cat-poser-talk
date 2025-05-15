using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.Interfaces;
using Cysharp.Threading.Tasks;

namespace CheshireCatPoserTalk.Infrastructure.Services.AwaiterService
{
    public class UniTaskAwaiterService : IAwaiterService
    {
        public ValueTask WaitSecondsAsync(float seconds, CancellationToken cancellationToken = default)
        {
            if (seconds <= 0)
            {
                return UniTask.CompletedTask.AsValueTask();
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return UniTask.FromCanceled(cancellationToken).AsValueTask();
            }

            return UniTask.Delay((int)(seconds * 1000), cancellationToken: cancellationToken).AsValueTask();
        }

        private UniTaskCompletionSource waitInputSource;

        public async ValueTask WaitForInputAsync(CancellationToken cancellationToken = default)
        {
            waitInputSource = new UniTaskCompletionSource();

            using (cancellationToken.Register(() => waitInputSource.TrySetCanceled()))
            {
                await waitInputSource.Task;
            }
            waitInputSource = null;
        }

        public void OnInputReceived() => waitInputSource?.TrySetResult();
    }
}
