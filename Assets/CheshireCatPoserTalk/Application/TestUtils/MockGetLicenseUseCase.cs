using System.Collections.Generic;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Domain.UseCases.GetLicense;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockGetLicenseUseCase : IGetLicenseUseCase
    {
        private List<string> licenses;

        public MockGetLicenseUseCase(List<string> licenses)
        {
            this.licenses = licenses;
        }

        public ValueTask<List<string>> GetLicensesAsync()
        {
            return new ValueTask<List<string>>(licenses);
        }
    }
}
