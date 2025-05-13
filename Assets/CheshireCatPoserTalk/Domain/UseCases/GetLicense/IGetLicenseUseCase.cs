using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.UseCases.GetLicense
{
    public interface IGetLicenseUseCase
    {
        ValueTask<List<string>> GetLicensesAsync();
    }
}
