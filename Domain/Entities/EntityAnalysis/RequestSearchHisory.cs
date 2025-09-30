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
        public string? Keyword { get; set; }
        public string? TypeSearch { get; set; } // 1: Company, 2: Person
        public string? KeySearch { get; set; } // The actual search key used
        public string? ExDataSearch { get; set; } // Extra data related to the search
        public int? ResultCount { get; set; } // Number of results returned
        public bool? IsSuccess { get; set; } // Indicates if the search was successful
        public int? StatusCode { get; set; } // Status code of the search operation
        public DateTime? SearchDate { get; set; }
    }
}
