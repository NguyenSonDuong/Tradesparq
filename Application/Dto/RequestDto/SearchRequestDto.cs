using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Request
{
    public class SearchRequestDto
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public int? source_type { get; set; }
            public string? dataSource { get; set; }
            public List<string>? date { get; set; } = new List<string>();
            public List<string>? filter_field { get; set; } = new List<string>();

            public int? page { get; set; }
            public int? page_size { get; set; } = 100;
            public string? sort { get; set; }
            public string? order { get; set; }
            public string? result_type { get; set; }

            public int? filter_size { get; set; }
            public string? prod_desc { get; set; }
            public string? hs_code { get; set; }
            public string? supplier_side { get; set; }
            public string? exporter_id { get; set; }
            public string? supplier_addr { get; set; }
            public bool? supplier_is_log { get; set; }
            public bool? supplier_is_nvl { get; set; }
            public string? buyer_side { get; set; }
            public string? importer_id { get; set; }
            public string? buyer_addr { get; set; }
            public bool? buyer_is_log { get; set; }
            public bool? buyer_is_nvl { get; set; }
            public List<string>? quantity { get; set; }
            public List<string>? weight { get; set; } 
            public List<string>? amount { get; set; }
            public List<string>? teu { get; set; } 
            public List<string>? orig_country_code { get; set; } 
            public List<string>? dest_country_code { get; set; }
            public List<string>? orig_port_id { get; set; } 
            public List<string>? trans_type_code { get; set; }
            public List<string>? incoterms { get; set; } 
            public string? master_bill_no { get; set; }
            public string? sub_bill_no { get; set; }
            public string? container_no { get; set; }
            public string? carrier_name { get; set; }
            public bool? result_type_need_num { get; set; }
            public string? vessel_name { get; set; }
            public string? brand { get; set; }
            public string? others { get; set; }
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
