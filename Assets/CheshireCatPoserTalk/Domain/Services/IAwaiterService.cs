using System.Threading;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Services
{
    public interface IAwaiterService
    {
        ValueTask WaitSecondsAsync(float seconds, CancellationToken cancellationToken = default);
        ValueTask WaitForInputAsync(CancellationToken cancellationToken = default);
    }
}
