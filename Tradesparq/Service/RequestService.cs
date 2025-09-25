using CrawlService.Dto;
using CrawlService.Dto.Request;
using CrawlService.Dto.Responsive;
using DabacoControl.api;
using DabacoControl.Builder;
using DabacoControl.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Dto.Request;
using Tradesparq.Dto.ResponsiveDto;

namespace Tradesparq.Service
{
    public class RequestService : IRequestService
    {

        private ApiBaseController _apiBaseController;
        private static string HOST = "rest.tradesparq.com";
        private static int PORT = 443;
        private HttpRequestSettingDtoBuilder httpRequestSettingDto;
        public RequestService()
        {
            _apiBaseController = new ApiBaseController();
            _apiBaseController.Port = PORT;
            _apiBaseController.Host = HOST;
            _apiBaseController.HeadersDefault = new Dictionary<string, string>();
            _apiBaseController.IsHttps = true; 
            httpRequestSettingDto = new HttpRequestSettingDtoBuilder()
                .AddHeader("Connection", "keep-alive")
                .AddHeader("Sec-Fetch-Dest", "empty")
                .AddHeader("Sec-Fetch-Mode", "cors")
                .AddHeader("Sec-Fetch-Site", "same-site")
                .AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36")
                .AddHeader("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Google Chrome\";v=\"138\"")
                .AddHeader("sec-ch-ua-mobile", "?0")
                .AddHeader("Origin", "https://data.tradesparq.com")
                .AddHeader("sec-ch-ua-platform", "\"Windows\"");
        }
        public void SetToken(string token)
        {
            _apiBaseController.HeadersDefault.Add("Ts-Token", token);
        }

        public async Task<DataSourceResponsiveDto.Root> GetDataSource()
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("console/user/identity")
                    .SetMethod(ApiMethodEnum.GET);
                HttpResponseMessage httpResponse = await _apiBaseController.RequestAsync(httpRequestSettingDto.Build());
                string httpRpString = await ApiBaseController.ResponsiveMapping(httpResponse, httpRequestSettingDto.Build());
                DataSourceResponsiveDto.Root root = JsonConvert.DeserializeObject<DataSourceResponsiveDto.Root>(httpRpString);
                return root;
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        public async Task<SearchResponsiveDto.Root> GetSearch(SearchRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data_v2/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .AddHeader("Referer", "https://data.tradesparq.com")
                    .SetBody(request);
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

        public async Task<SearchResponsiveDto.Root> GetImportPartner(ImportPartnerRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search/company")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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

        public async Task<SearchResponsiveDto.Root> GetImportShipment(ImportShipmentRequestDto.Root request, String token)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search/company")
                    .SetMethod(ApiMethodEnum.POST)
                    .AddHeader("Ts-Token", token)
                    .SetBody(request);
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


        public async Task<SearchResponsiveDto.Root> GetExportParner(ExportParnerRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search/company")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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

        public async Task<SearchResponsiveDto.Root> GetExportShipment(ExportShipmentRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search/company")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchBOL(SearchBOLRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchCompanyResponsivDto.Root> GetSearchCompany(SearchCompanyRequestDto.Root request, string token)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data_v2/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .AddHeader("Ts-Token", token)
                    .AddHeader("Referer", "https://data.tradesparq.com")
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchMonthlyTrends(SearchMonthlyTrendRequestDtos.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchProductTransactions(SearchProductTransactionsRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchProductWeight(SearchProductWeightRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchProductAmount(SearchProductAmountRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
        public async Task<SearchResponsiveDto.Root> GetSearchProductTransactionQuantity(SearchProductTransactionQuantityRequestDto.Root request)
        {
            try
            {
                httpRequestSettingDto
                    .SetPath("data/search")
                    .SetMethod(ApiMethodEnum.POST)
                    .SetBody(request);
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
