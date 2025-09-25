using CrawlService.Dto;
using CrawlService.Dto.Request;
using CrawlService.Dto.Responsive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradesparq.Service
{
    public interface IRequestService
    {
        void SetToken(string token);

        Task<DataSourceResponsiveDto.Root> GetDataSource();

        Task<SearchResponsiveDto.Root> GetSearch(SearchRequestDto.Root request);

    }
}
