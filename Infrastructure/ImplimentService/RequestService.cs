using Application.Dto.Request;
using Application.Dto.ResponsiveDto;
using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentService
{
    public class RequestService :  BaseService, IRequestService
    {

        public Task<SearchCompanyResponsivDto.Root> GetCompany(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                return Task.FromResult(new SearchCompanyResponsivDto.Root());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
