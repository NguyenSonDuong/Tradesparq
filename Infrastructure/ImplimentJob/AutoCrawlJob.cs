using Application.Dto.Request;
using Application.IJob;
using Application.IService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Dto.Request;

namespace Infrastructure.ImplimentJob
{
    public class AutoCrawlJob : IAutoCrawlJob
    {
        private readonly ILogger<AutoCrawlJob> _log;
        private ICompanyService _companyService;
        public AutoCrawlJob(ILogger<AutoCrawlJob> log, ICompanyService companyService) 
        { 
            this._log = log;
            this._companyService = companyService;
        }
        public async Task ExecuteAsync(CancellationToken ct)
        {
            try
            {
                _log.LogInformation("MyJob start at {time:O}", DateTime.UtcNow);
                // TODO: gọi repo/service thật của bạn ở đây
                SearchRequestDto.Root requestRoot = new SearchRequestDto.Root();
                requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
                requestRoot.date = new List<string>();
                requestRoot.date.Add("2024-09-21");
                requestRoot.date.Add("2025-09-21");
                requestRoot.order = "desc";
                requestRoot.page = page;
                requestRoot.page_size = 20;
                requestRoot.prod_desc = keySearch;
                requestRoot.result_type = "supplier";
                requestRoot.result_type_need_num = true;
                requestRoot.source_type = 1;

                _companyService.GetAllCompany(requestRoot);

                _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
            }
            catch(Exception ex)
            {
                _log.LogError($"AutoCrawlJob - Error: {ex.Message}");
            }  
        }
    }
}
