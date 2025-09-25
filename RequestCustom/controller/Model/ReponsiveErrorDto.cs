using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabacoControl.Model
{
    public class ReponsiveErrorDto : ResponsiveBaseDto
    {
        public string type { get; set; }
        public string traceId { get; set; }
        public string detail { get; set; }
        public JObject errors { get; set; }
    }
}
