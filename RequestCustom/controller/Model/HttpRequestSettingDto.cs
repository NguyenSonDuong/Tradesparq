using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DabacoControl.model
{
    /// <summary>
    /// Class để tạo config cho request 
    /// </summary>
    public class HttpRequestSettingDto
    {
        // Khi vực config cho request header
        private string _contentType;
        private string _path;
        private ApiMethodEnum _method;

        // khu vực config cho API
        private string _root;
        private string _version;
        private string _controller;

        private Dictionary<string, dynamic> _headers = new Dictionary<string, dynamic>();
        private string _authorization;
        private Dictionary<string, dynamic> _queryParams = new Dictionary<string, dynamic>();
        private Object _body = new Object();
        private string _bodyJson;

        private string _urlPath;

        public Dictionary<string, dynamic> Headers { get => _headers; set => _headers = value; }
        public Dictionary<string, dynamic> QueryParams { get => _queryParams; set => _queryParams = value; }
        public Object Body
        {
            get => _body;
            set
            {
                _body = value;
                _bodyJson = JsonConvert.SerializeObject(value);
            }
        }
        public string BodyJson
        {
            get
            {
                return _bodyJson;
            }
            set
            {
                _bodyJson = value;
                if (string.IsNullOrEmpty(ContentType) || ContentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    ContentType = "application/json";
                }
            }
        }

        public string ContentType { get => _contentType; set => _contentType = value; }
        public string Path { get => _path; set => _path = value; }
        public string Root { get => _root; set => _root = value; }
        public string Version { get => _version; set => _version = value; }
        public string Controller { get => _controller; set => _controller = value; }
        public ApiMethodEnum Method { get => _method; set => _method = value; }
        public string Authorization { get => _authorization; set => _authorization = value; }
        public string UrlPath 
        {
            get => $"{Root}/{Version}/{Controller}/{Path}"; 
        }
    }
}
