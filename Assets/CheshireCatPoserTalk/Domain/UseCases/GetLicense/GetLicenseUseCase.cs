using CheshireCatPoserTalk.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheshireCatPoserTalk.Domain.UseCases.GetLicense
{
    public class GetLicenseUseCase : IGetLicenseUseCase
    {
        private ILicenseRepository licenseRepository;

        public GetLicenseUseCase(ILicenseRepository licenseRepository)
        {
            this.licenseRepository = licenseRepository;
        }

        public ValueTask<List<string>> GetLicensesAsync()
        {
            return licenseRepository.GetLicensesAsync();
        }
    }
}
