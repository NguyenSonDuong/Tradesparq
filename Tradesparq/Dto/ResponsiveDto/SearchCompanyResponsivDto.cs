using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradesparq.Dto.ResponsiveDto
{
    public class SearchCompanyResponsivDto
    {
        public class Bucket
        {
            public string val { get; set; }
            public int total { get; set; }
            public int count { get; set; }
            public Info info { get; set; }
        }

        public class Data
        {
            public List<Bucket> buckets { get; set; }
            public int numBuckets { get; set; }
        }

        public class Info
        {
            public string uuid { get; set; }
            public string name { get; set; }
            public string uid { get; set; }
            public string uname { get; set; }
            public List<string> phone { get; set; }
            public List<string> email { get; set; }
            public List<string> fax { get; set; }
            public List<string> postal_code { get; set; }
            public List<string> city { get; set; }
            public string address { get; set; }
            public string country { get; set; }
        }

        public class Root
        {
            public int code { get; set; }
            public Data data { get; set; }
        }
    }
}
