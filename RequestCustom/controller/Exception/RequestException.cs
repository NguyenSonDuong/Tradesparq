using CrawlService.Controller.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabacoControl.CustomException
{
    public class RequestException : ControllerException
    {
        public RequestException()
        {
            this.ControllerName = "ApiBaseController";
        }

        // Constructor nhận thông báo lỗi
        public RequestException(string message)
            : base(message)
        {
            this.ControllerName = "ApiBaseController";
        }

        // Constructor nhận thông báo lỗi và inner exception
        public RequestException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            this.ControllerName = "ApiBaseController";
        }
    }
}
