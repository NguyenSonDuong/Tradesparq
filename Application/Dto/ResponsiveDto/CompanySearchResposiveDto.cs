using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ResponsiveDto
{
    public class CompanySearchResposiveDto
    {
        public class Data
        {
            public List<Doc> docs { get; set; }
            public int numFound { get; set; }
            public object coms { get; set; } 
        }

        public class Doc
        {
            public string id { get; set; }
            public string batch_id { get; set; }
            public string data_source { get; set; }
            public string date { get; set; }
            public string supplier_id { get; set; }
            public string supplier { get; set; }
            public string supplier_addr { get; set; }
            public string exporter_id { get; set; }
            public string buyer_id { get; set; }
            public string buyer { get; set; }
            public string buyer_addr { get; set; }
            public string importer_id { get; set; }
            public string hs_code { get; set; }
            public string prod_desc { get; set; }
            public string orig_country_code { get; set; }
            public string dest_country_code { get; set; }
            public string orig_port { get; set; }
            public string dest_port { get; set; }
            public string customs { get; set; }
            public object teu { get; set; }
            public double amount { get; set; }
            public double? price { get; set; }
            public double? weight { get; set; }
            public double quantity { get; set; }
            public string quantity_unit { get; set; }
            public string master_bill_no { get; set; }
            public object container_no { get; set; }
            public string trans_type { get; set; }
            public object incoterms { get; set; }
            public object carrier_name { get; set; }
            public object vessel_name { get; set; }
            public object brand { get; set; }
        }

        public class Root
        {
            public int code { get; set; }
            public Data data { get; set; }
        }
    }
}
