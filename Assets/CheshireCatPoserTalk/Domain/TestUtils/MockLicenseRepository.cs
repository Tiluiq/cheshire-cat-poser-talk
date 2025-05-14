using CheshireCatPoserTalk.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.TestUtils
{
    public class MockLicenseRepository : ILicenseRepository
    {
        private List<string> licenses;

        public MockLicenseRepository(List<string> licenses)
        {
            this.licenses = licenses;
        }


        public ValueTask<List<string>> GetLicensesAsync()
        {
            return new ValueTask<List<string>>(licenses);
        }
    }
}
