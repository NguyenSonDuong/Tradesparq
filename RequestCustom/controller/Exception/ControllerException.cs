using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlService.Controller.Exception
{
    public class ControllerException : System.Exception
    {
        // Lưu thông tin controller để dễ dàng debug
        private string _controllerName;
        // Constructor không tham số
        public ControllerException()
        {
        }

        // Constructor nhận thông báo lỗi
        public ControllerException(string message)
            : base(message)
        {
        }

        // Constructor nhận thông báo lỗi và inner exception
        public ControllerException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public string ControllerName { get => _controllerName; set => _controllerName = value; }
    }
    
}
