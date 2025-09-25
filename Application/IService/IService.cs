using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IService
    {
        public string Token { get; set; }
        public string DataSource { get; set; }
    }
}
