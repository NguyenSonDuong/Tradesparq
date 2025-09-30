using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Authen
{
    public class AuthenTradesparq : BaseEntity
    {
        public string Token { get; set; } 
        public string dataSourch { get; set; }
        public int Status { get; set; }
    }
}
