using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Interfaces
{
    public interface IAnimationController
    {
        public ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken = default);
    }
}
