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

namespace Infrastructure.ImplimentService.Base
{
    public class RequestService :  BaseService, IRequestService
    {
        private IApiBaseController _apiBaseController;
        private static string HOST = "rest.tradesparq.com";
        private static int PORT = 443;



        public RequestService(IApiBaseController apiBaseController)
        {
            _apiBaseController = apiBaseController;
            _apiBaseController.SetPort(PORT);
            _apiBaseController.SetHost(HOST);
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {   
                { "Accept", "application/json, text/plain, */*" },
                { "Accept-Language", "en-US,en;q=0.9,vi;q=0.8" },
                { "Connection", "keep-alive" },
                { "Host", "rest.tradesparq.com" },
                { "Origin", "https://data.tradesparq.com" },
                { "Referer", "https://data.tradesparq.com/" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-site" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36" },
                { "sec-ch-ua", "\"Chromium\";v=\"140\", \"Not=A?Brand\";v=\"24\", \"Google Chrome\";v=\"140\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" }
            };
            _apiBaseController.SetHeadersDefault(headers);
            _apiBaseController.SetIsHttps(true);
        }

        public async Task<SearchCompanyResponsivDto.Root> GetCompany(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                HttpRequestSettingDtoBuilder httpRequestSettingDto = new HttpRequestSettingDtoBuilder()
                     .SetPath(PathKey.SearchCompanyPath)
                     .SetMethod(ApiMethodEnum.POST)
                     .AddHeader("ts-token", Token)
                     .SetBody(searchRequestDto);
                HttpResponseMessage httpResponse = await _apiBaseController.RequestAsync(httpRequestSettingDto.Build());
                string httpRpString = await ApiBaseController.ResponsiveMapping(httpResponse, httpRequestSettingDto.Build());
                SearchCompanyResponsivDto.Root root = JsonConvert.DeserializeObject<SearchCompanyResponsivDto.Root>(httpRpString);
                return root;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SearchResponsiveDto.Root> GetShipment(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                HttpRequestSettingDtoBuilder httpRequestSettingDto = new HttpRequestSettingDtoBuilder()
                     .SetPath(PathKey.SearchCompanyPath)
                     .SetMethod(ApiMethodEnum.POST)
                     .AddHeader("ts-token", Token)
                     .SetBody(searchRequestDto);
                HttpResponseMessage httpResponse = await _apiBaseController.RequestAsync(httpRequestSettingDto.Build());
                string httpRpString = await ApiBaseController.ResponsiveMapping(httpResponse, httpRequestSettingDto.Build());
                SearchResponsiveDto.Root root = JsonConvert.DeserializeObject<SearchResponsiveDto.Root>(httpRpString);
                return root;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
