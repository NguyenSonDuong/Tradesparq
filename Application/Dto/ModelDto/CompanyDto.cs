using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ModelDto
{
    public class CompanyDto
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
        public bool? IsDeleted { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
