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
        public int CommandId { get; set; } // Foreign key to Command entity
        public string? ExDataSearch { get; set; } // Extra data related to the search
        public int? ResultCount { get; set; } // Number of results returned
        public bool? IsSuccess { get; set; } // Indicates if the search was successful
        public int? StatusCode { get; set; } // Status code of the search operation
        public DateTime? SearchDate { get; set; }
    }
}
