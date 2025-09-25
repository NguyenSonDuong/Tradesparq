
using CrawlService.Controller;
using DabacoControl.CustomException;
using DabacoControl.model;
using DabacoControl.Model;
using DabacoModel.modeldto;
using Newtonsoft.Json;
using NLog;
using Polly;
using Polly.Retry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace DabacoControl.api
{

    /// <summary>
    /// Đây là class base của request 
    /// lớp sử dụng đế các thức Builder để tạo nên thông tin request gửi lên
    /// bao gồm url, header, body
    /// Lớp có authen và có LoginResponsiveDto là biến static sẽ được sử dụng khi xác thực 
    /// trên các API khác, đây là biến static có thể sử dụng ở trên ứng dụng
    /// giống như một biến toàn cục đẩm nhận xử lý request có authen
    /// Sau khi login thành công thì tất cả các request còn lại sẽ tự động được thêm authen theo
    /// Token đã được lưu ở LoginResponsiveDto
    /// </summary>
    public class ApiBaseController : IApiBaseController
    {
        private static readonly Logger _logger = LogManager.GetLogger("ControllerLogger");

        static readonly AsyncRetryPolicy<HttpResponseMessage> retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>() // Lỗi mạng
            .OrResult(r => (int)r.StatusCode >= 500) // Retry nếu server lỗi (5xx)
            .OrResult(response => response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(2),
                onRetry: async (response, timespan, retryCount, context) =>
                {
                    if (response.Result?.StatusCode == HttpStatusCode.Unauthorized || response.Result?.StatusCode == HttpStatusCode.Forbidden)
                    {
                        _logger.Error($"[Retry] Lỗi {response.Result.StatusCode} - {response.Result.ReasonPhrase}, Lấy lại token mới");
                        if (RefreshTokenAsync == null)
                            return;
                        if (await ApiBaseController.RefreshTokenAsync())
                        {
                            _logger.Info($"[Retry] Lấy lại token thành công, thử lại lần {retryCount}");
                        }
                        else
                        {
                            _logger.Error($"[Retry] Lấy lại token không thành công, dừng retry, thử lại lần {retryCount}");
                        }
                    }
                });
        public static Func<Task<bool>> RefreshTokenAsync;

        // Config cho authen có thể chỉnh sửa trong config
        public static string AuthenticationKey = "Bearer";

        private readonly HttpClient httpClient;
        private string _host;
        private int _port;
        private bool _isHttps ;
        private Dictionary<string, string> _headersDefault;
        private bool _isSaveHeader = true;
        private static int RefreshTokenQuantity = 5;
        private static int ReCallRequestTimeDelay = 1000; // theo mms
        public ApiBaseController()
        {
            httpClient = new HttpClient();
            IsHttps = false;
        }

        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public bool IsHttps { get => _isHttps; set => _isHttps = value; }
        public Dictionary<string, string> HeadersDefault { get => _headersDefault; set => _headersDefault = value; }
        public bool IsSaveHeader { get => _isSaveHeader; set => _isSaveHeader = value; }

        /// <summary>
        /// Hàm này có nhiệm vụ chuyển đổi các thong tin liên quan đến đường dẫn API thành 1 
        /// url chuẩn gọi đến server
        /// </summary>
        /// <param name="httpRequestSetting">Model thông tin request</param>
        /// <returns></returns>
        /// <exception cref="RequestException">Lỗi sẽ bao gồm Tầng bị lỗi và Controller bị lỗi tiện cho debug</exception>
        public string GetUrl(HttpRequestSettingDto httpRequestSetting)
        {
            try
            {
                string httpver = "";
                if (IsHttps)
                {
                    httpver = "https";
                }
                else
                {
                    httpver = "http";
                }
                
                string path = string.IsNullOrEmpty(httpRequestSetting.Path) ?
                    $"{httpRequestSetting.Root}/{httpRequestSetting.Version}/{httpRequestSetting.Controller}" : // Trường hợp không có path 
                    $"{httpRequestSetting.Root}/{httpRequestSetting.Version}/{httpRequestSetting.Controller}/{httpRequestSetting.Path.TrimStart('/').TrimEnd('/')}"; // Trường hợp có path
                if (String.IsNullOrEmpty(httpRequestSetting.Root))
                    path = httpRequestSetting.Path;
                var builder = new UriBuilder(httpver, $"{Host}", Port, path.TrimEnd('/'));
                NameValueCollection queryParams;
                try
                {
                    queryParams = HttpUtility.ParseQueryString(string.Empty);
                }
                catch (Exception ex) 
                {
                    throw new RequestException("Thiếu dữ liệu trong đường dẫn! Liên hệ kỹ thuật viên để kiểm tra");
                }
                 
                if (httpRequestSetting.QueryParams != null && httpRequestSetting.QueryParams.Count > 0)
                {
                    foreach (var item in httpRequestSetting.QueryParams)
                    {
                        queryParams[item.Key] = item.Value;
                    }

                    builder.Query = queryParams.ToString();
                }
                return builder.ToString();
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                throw ;
            }
        }



        public async Task<HttpResponseMessage> RequestAsync(HttpRequestSettingDto httpRequestSetting)
        {
            try
            {
                return await retryPolicy.ExecuteAsync(() => RequestBaseAsync(httpRequestSetting));
            }catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Hàm request chung cho các api
        /// </summary>
        /// <param name="httpRequestSetting"></param>
        /// <returns></returns>
        /// <exception cref="RequestException"></exception>
        private async Task<HttpResponseMessage> RequestBaseAsync(HttpRequestSettingDto httpRequestSetting)
        {
            try
            {
                if (string.IsNullOrEmpty(httpRequestSetting.Path))
                {
                    if (string.IsNullOrEmpty(httpRequestSetting.Root) || string.IsNullOrEmpty(httpRequestSetting.Version)  || string.IsNullOrEmpty(httpRequestSetting.Controller))
                        throw new RequestException($"Thếu dữ liệu cho Path request");
                }
                if (httpClient == null)
                {
                    throw new RequestException(MessageErrorModel.RequestEmtyOrError);
                }

                String url = GetUrl(httpRequestSetting);

                if (string.IsNullOrEmpty(url))
                    throw new RequestException(MessageErrorModel.RequestUrlErrorOrConvertUrlError);

                if (_isSaveHeader)
                {
                    if (httpRequestSetting.Headers != null && httpRequestSetting.Headers.Count > 0)
                        foreach (var header in httpRequestSetting.Headers)
                        {
                            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                }
                
                
                if(HeadersDefault != null)
                {
                    foreach (var header in HeadersDefault)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                if (!string.IsNullOrEmpty(httpRequestSetting.Authorization))
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ApiBaseController.AuthenticationKey, httpRequestSetting.Authorization);
                _logger.Info($"[Request] Gửi request đến: {url} với phương thức {httpRequestSetting.Method}");
                HttpResponseMessage httpResponseMessage = null;
                try
                {
                    switch (httpRequestSetting.Method)
                    {
                        case ApiMethodEnum.GET:
                            httpResponseMessage = await httpClient.GetAsync(url);
                            break;
                        case ApiMethodEnum.POST:
                            var contentPost = new StringContent(httpRequestSetting.BodyJson, Encoding.UTF8, "application/json");
                            httpResponseMessage = await httpClient.PostAsync(url, contentPost);
                            break;
                        case ApiMethodEnum.PUT:
                            var contentPut = new StringContent(httpRequestSetting.BodyJson, Encoding.UTF8, "application/json");
                            httpResponseMessage = await httpClient.PutAsync(url, contentPut);
                            break;
                        case ApiMethodEnum.DELETE:
                            httpResponseMessage = await httpClient.DeleteAsync(url);
                            break;
                        default:
                            throw new RequestException(MessageErrorModel.RequestMethodNotFound);
                    }
                }
                catch (Exception ex)
                {
                    throw new RequestException(MessageErrorModel.RequestConnectServerError.Replace("[HOST]",Host).Replace("[POST]",Port+""));
                }
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message); 
                throw;
            }
        }
        /// <summary>
        /// Hàm mapping chuyển đổi từ HttpResponseMessage 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        /// <exception cref="RequestException"></exception>
        /// <exception cref="ResponsiveException"></exception>
        public static async Task<string> ResponsiveMapping(HttpResponseMessage httpResponseMessage, HttpRequestSettingDto httpRequestSetting)
        {
            try
            {
                if (httpResponseMessage == null)
                    throw new RequestException(MessageErrorModel.RequestEmtyOrError);
                if(httpRequestSetting != null)
                    _logger.Info($"API: {httpRequestSetting.UrlPath} với phương thức {httpRequestSetting.Method} Responsive Status = [{httpResponseMessage.StatusCode}]");
                else
                    _logger.Info($"Request Responsive Status = [{httpResponseMessage.StatusCode}]");
                switch (httpResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            string responsiveBody = await httpResponseMessage.Content.ReadAsStringAsync();
                            return responsiveBody;
                        case HttpStatusCode.BadRequest:
                            responsiveBody = await httpResponseMessage.Content.ReadAsStringAsync();
                            ReponsiveErrorDto reponsiveError = JsonConvert.DeserializeObject<ReponsiveErrorDto>(responsiveBody);
                            if (reponsiveError.errors != null)
                            {
                                throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorValidation);
                            }
                            else
                            {
                                throw new ResponsiveException((int)httpResponseMessage.StatusCode, reponsiveError.detail);
                            }
                        case HttpStatusCode.Unauthorized:
                            throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorUnauthorized);
                        case HttpStatusCode.Forbidden:
                            throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorForbidden);
                        case HttpStatusCode.NotFound:
                            throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorNotFound);
                        case HttpStatusCode.InternalServerError:
                            throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorInternalServerError);
                        case HttpStatusCode.ServiceUnavailable:
                            throw new ResponsiveException((int)httpResponseMessage.StatusCode, MessageErrorModel.ResponsiveErrorServiceUnavailable);
                        default:
                            return null;
                    }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }
    }
}
