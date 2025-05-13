using NUnit.Framework;
using CheshireCatPoserTalk.Domain.TestUtils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.UseCases.GetLicense.Tests
{
    public class GetLicenseUseCaseTest
    {
        private IGetLicenseUseCase getLicenseUseCase;

        private List<string> licenses = new List<string>
        {
            "License1\n" +
            "This is a test license 1.\n",
            "License2\n" +
            "This is a test license 2.\n",
            "License3\n" +
            "This is a test license 3.\n",
        };

        [SetUp]
        public void Setup()
        {
            getLicenseUseCase = new GetLicenseUseCase(new MockLicenseRepository(licenses));
        }

        [Test]
        public async Task GetLicensesAsync_ライセンスが取得できる()
        {
            var expectedLicenses = new List<string>(licenses);

            var result = await getLicenseUseCase.GetLicensesAsync();

            Assert.AreEqual(expectedLicenses.Count, result.Count);
            for (int i = 0; i < expectedLicenses.Count; i++)
            {
                Assert.AreEqual(expectedLicenses[i], result[i]);
            }
        }
    }
}
