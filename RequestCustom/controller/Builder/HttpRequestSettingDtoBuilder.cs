using DabacoControl.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabacoControl.Builder
{
    /// <summary>
    /// Lớp Builder là lớp sẽ được gọi ra 
    /// khi người dùng muốn cấu hình 1 request 
    /// </summary>
    public class HttpRequestSettingDtoBuilder
    {
        private HttpRequestSettingDto _dto;
        public HttpRequestSettingDtoBuilder()
        {
            _dto = new HttpRequestSettingDto();
        }
        public HttpRequestSettingDtoBuilder SetContentType(string contentType)
        {
            _dto.ContentType = contentType;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetPath(string path)
        {
            _dto.Path = path;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetRoot(string root)
        {
            _dto.Root = root;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetVersion(string version)
        {
            _dto.Version = version;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetController(string controller)
        {
            _dto.Controller = controller;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetAuthorization(string authorization)
        {
            _dto.Authorization = authorization;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetMethod(ApiMethodEnum method)
        {
            _dto.Method = method;
            return this;
        }

        public HttpRequestSettingDtoBuilder SetBodyJson(string bodyJson)
        {
            _dto.BodyJson = bodyJson;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetBody(Dictionary<string, dynamic> body)
        {
            _dto.Body = body;
            _dto.BodyJson = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return this;
        }
        public HttpRequestSettingDtoBuilder SetBody(Object body)
        {
            _dto.Body = body;
            _dto.BodyJson = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return this;
        }
        public HttpRequestSettingDtoBuilder SetHeaders(Dictionary<string, dynamic> headers)
        {
            _dto.Headers = headers;
            return this;
        }
        public HttpRequestSettingDtoBuilder SetQueryParams(Dictionary<string, dynamic> queryParams)
        {
            _dto.QueryParams = queryParams;
            return this;
        }
        public HttpRequestSettingDtoBuilder AddHeader(string key, string value)
        {
            if (_dto.Headers == null)
            {
                _dto.Headers = new Dictionary<string, dynamic>();
            }

            if (_dto.Headers.ContainsKey(key))
            {
                _dto.Headers[key] = value; // Update existing header
            }
            else
            {
                _dto.Headers.Add(key, value); // Add new header
            }
            return this;
        }

        public HttpRequestSettingDtoBuilder AddQueryParam(string key, string value)
        {
            if (_dto.QueryParams == null)
            {
                _dto.QueryParams = new Dictionary<string, dynamic>();
            }
            if (_dto.QueryParams.ContainsKey(key))
            {
                _dto.QueryParams[key] = value; // Update existing query param
            }
            else
            {
                _dto.QueryParams.Add(key, value); // Add new query param
            }
            return this;
        }
        
        public HttpRequestSettingDtoBuilder SetStringHeader(string bodyJson)
        {
            try
            {
                if (string.IsNullOrEmpty(bodyJson))
                {
                    throw new ArgumentException("Body JSON cannot be null or empty.", nameof(bodyJson));
                }
                String[] listHeader = bodyJson.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in listHeader)
                {
                    String[] keyAndValueHeaders = item.Split(':');
                    if (keyAndValueHeaders.Length == 2)
                    {
                        String key = keyAndValueHeaders[0].Trim();
                        String value = keyAndValueHeaders[1].Trim();
                        AddHeader(key, value);
                    }
                }
                return this;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public HttpRequestSettingDtoBuilder SetFileHeader(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException("File path empty");
                }
                String[] headerString = File.ReadAllLines(path);
                foreach (var item in headerString)
                {
                    String[] keyAndValueHeaders = item.Split(':');
                    if (keyAndValueHeaders.Length == 2)
                    {
                        String key = keyAndValueHeaders[0].Trim();
                        String value = keyAndValueHeaders[1].Trim();
                        AddHeader(key, value);
                    }
                }
                return this;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public HttpRequestSettingDto Build()
        {
            return _dto;
        }
    }
}
