using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlService.Dto
{
    public class DataSourceResponsiveDto
    {
        public class Account
        {
            public int id { get; set; }
            public string name { get; set; }
            public int mark { get; set; }
            public int userNum { get; set; }
        }

        public class CD
        {
            public string data_source { get; set; }
            public string download { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string nickname { get; set; }
            public string email { get; set; }
            public object phone { get; set; }
            public int level { get; set; }
            public string lang { get; set; }
            public string createTime { get; set; }
            public string lastLoginTime { get; set; }
            public Account account { get; set; }
            public object deptInfo { get; set; }
            public List<string> userRole { get; set; }
            public List<Permission> permissions { get; set; }
            public object oemDomains { get; set; }
            public TradesoResultVo tradesoResultVo { get; set; }
            public object weChatUserInfo { get; set; }
        }

        public class Permission
        {
            public List<T> TS { get; set; }
        }

        public class Root
        {
            public int code { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }

        public class T
        {
            public List<CD> CDS { get; set; }
            public string end_time { get; set; }
        }

        public class TradesoResultVo
        {
            public int id { get; set; }
        }

    }
}
