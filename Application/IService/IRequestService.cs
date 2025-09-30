using Application.Dto.Request;
using Application.Dto.ResponsiveDto;
using CrawlService.Dto.Responsive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IRequestService : IService
    {
        Task<SearchCompanyResponsivDto.Root> GetCompany(SearchRequestDto.Root searchRequestDto);
        Task<SearchResponsiveDto.Root> GetShipment(SearchRequestDto.Root searchRequestDto);
    }
}
