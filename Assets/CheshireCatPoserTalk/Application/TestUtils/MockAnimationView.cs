using System.Threading;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.Interfaces;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockAnimationView : IAnimationView
    {
        public bool IsPlayAnimationAsyncCalled { get; private set; }

        public ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken = default)
        {
            IsPlayAnimationAsyncCalled = true;
            return new ValueTask();
        }
    }
}
