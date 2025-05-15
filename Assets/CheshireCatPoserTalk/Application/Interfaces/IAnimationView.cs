using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Application.Interfaces
{
    public interface IAnimationView
    {
        ValueTask PlayAnimationAsync(string targetId, string animationName, CancellationToken cancellationToken = default);
    }
}
