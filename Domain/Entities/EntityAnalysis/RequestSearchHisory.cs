using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EntityAnalysis
{
    public class RequestSearchHisory : BaseEntity
    {
        public string Keyword { get; set; }
        public int TypeSearch { get; set; }
        public int ResultCount { get; set; }
        public int TotalCount { get; set; }
        public int NumberRequest { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
