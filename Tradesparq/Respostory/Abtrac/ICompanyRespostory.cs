using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tradesparq.Dto.ResponsiveDto.SearchCompanyResponsivDto;

namespace Tradesparq.Respostory.Abtrac
{
    public interface ICompanyRespostory
    {
        Task<int?> CreateCompany(Bucket company);

        Task<bool> CreateCityForCompany(int CompanyId, string city);
        Task<bool> CreateCitiesForCompany(int CompanyId, List<string> cities);
        Task<bool> CreateEmailForCompany(int CompanyId, string email);
        Task<bool> CreateEmailsForCompany(int CompanyId, List<string> email);
        Task<bool> CreateFaxForCompany(int CompanyId, string fax);
        Task<bool> CreateFaxsForCompany(int CompanyId, List<string> fax);
        Task<bool> CreatePhoneNumberForCompany(int CompanyId, string phoneNumber);
        Task<bool> CreatePhoneNumbersForCompany(int CompanyId, List<string> phoneNumber);
        Task<bool> CreatePostalCodeForCompany(int CompanyId, string postalCodes);
        Task<bool> CreatePostalCodesForCompany(int CompanyId, List<string> postalCodes);
    }
}
