using DabacoControl.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrawlService.Controller
{
    public interface IApiBaseController
    {
        /// <summary>
        /// Đây là hàm request chung nhận vào 1 HttpRequestSettingDto có chức năng phân tách các thành phần củab
        /// url, header, body để dễ dàng sử dụng nâng cấp
        /// </summary>
        /// <param name="requestSettingDto"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> RequestAsync(HttpRequestSettingDto requestSettingDto);
    }
}
