using CrawlService.Controller.Exception;
using DabacoControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabacoControl.CustomException
{
    public class ResponsiveException : ControllerException
    {
        private int _status;

        public ResponsiveException()
        {
            this.ControllerName = "ResposiveError";
        }

        // Constructor nhận thông báo lỗi
        public ResponsiveException(string message)
            : base(message)
        {
            this.ControllerName = "ResposiveError";
        }

        // Constructor nhận thông báo lỗi và inner exception
        public ResponsiveException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            this.ControllerName = "ResposiveError";
        }
        public ResponsiveException(int status, string message)
            : base(message)
        {
            this.Status = status;
            this.ControllerName = "ResposiveError";
        }

        public int Status { get => _status; set => _status = value; }
    }
}
