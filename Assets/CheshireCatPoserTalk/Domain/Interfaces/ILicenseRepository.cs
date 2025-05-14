using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Interfaces
{
    public interface ILicenseRepository
    {
        ValueTask<List<string>> GetLicensesAsync();
    }
}
