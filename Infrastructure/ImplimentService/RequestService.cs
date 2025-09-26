using Application.Dto.Keys;
using Application.Dto.Request;
using Application.Dto.ResponsiveDto;
using Application.IService;
using CrawlService.Controller;
using CrawlService.Dto.Responsive;
using DabacoControl.api;
using DabacoControl.Builder;
using DabacoControl.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Dto.Request;

namespace Infrastructure.ImplimentService
{
    public class RequestService :  BaseService, IRequestService
    {
        private IApiBaseController _apiBaseController;
        private static string HOST = "rest.tradesparq.com";
        private static int PORT = 443;
        private HttpRequestSettingDtoBuilder httpRequestSettingDto;
        public RequestService(IApiBaseController apiBaseController)
        {
            _apiBaseController = apiBaseController;
            _apiBaseController.SetPort(RequestService.PORT);
            _apiBaseController.SetHost(RequestService.HOST);
            _apiBaseController.SetHeadersDefault(new Dictionary<string, string>());
            _apiBaseController.SetIsHttps(true);
            httpRequestSettingDto = new HttpRequestSettingDtoBuilder()
                .AddHeader("Connection", "keep-alive")
                .AddHeader("Sec-Fetch-Dest", "empty")
                .AddHeader("Sec-Fetch-Mode", "cors")
                .AddHeader("Sec-Fetch-Site", "same-site")
                .AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36")
                .AddHeader("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Google Chrome\";v=\"138\"")
                .AddHeader("sec-ch-ua-mobile", "?0")
                .AddHeader("Origin", "https://data.tradesparq.com")
                .AddHeader("Referer", "https://data.tradesparq.com")
                .AddHeader("sec-ch-ua-platform", "\"Windows\"");

        }

        public async Task<SearchCompanyResponsivDto.Root> GetCompany(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                HttpRequestSettingDto httpRequestSettingDto = new HttpRequestSettingDtoBuilder()
                     .SetPath(PathKey.SearchCompanyPath)
                     .SetMethod(ApiMethodEnum.POST)
                     .SetBody(searchRequestDto)
                     .Build();
                HttpResponseMessage httpResponse = await _apiBaseController.RequestAsync(httpRequestSettingDto);
                string httpRpString = await ApiBaseController.ResponsiveMapping(httpResponse, httpRequestSettingDto);
                SearchCompanyResponsivDto.Root root = JsonConvert.DeserializeObject<SearchCompanyResponsivDto.Root>(httpRpString);
                return root;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
