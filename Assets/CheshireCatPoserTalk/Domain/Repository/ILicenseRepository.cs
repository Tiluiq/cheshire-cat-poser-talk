using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.Repository
{
    public interface ILicenseRepository
    {
        ValueTask<List<string>> GetLicensesAsync();
    }
}
