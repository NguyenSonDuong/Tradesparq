using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.command
{
    public class Command : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public string SearchKey { get; set; }
        public int IndexSearch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TypeSearch { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
