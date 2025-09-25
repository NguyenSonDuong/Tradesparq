using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRequest.Dto.Request
{
    public class SearchCompanyRequestDto
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public int source_type { get; set; } = 1;
            public string dataSource { get; set; }
            public List<string> date { get; set; }
            public int page { get; set; } = 1;
            public int page_size { get; set; } = 20;
            public string order { get; set; }
            public string prod_desc { get; set; }
            public bool result_type_need_num { get; set; }
            public string result_type { get; set; }
            public long timestamp
            {
                get
                {
                    return (DateTimeOffset.UtcNow.ToUnixTimeSeconds() * 1000) + (long)new Random().Next(0, 999);
                }
                set
                {

                }
            }
        }


    }
}
