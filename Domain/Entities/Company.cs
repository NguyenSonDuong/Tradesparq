using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company : BaseEntity
    {
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Name { get; set; }
        public string? Uid { get; set; }
        public string? Uname { get; set; }
        public string? Uuid { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public string? Var { get; set; }
    }
}
