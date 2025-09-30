using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentService.Base
{
    public abstract  class BaseService : IService
    {
        private string _token;
        private string _dataSource;
        public string Token { get => _token; set => _token = value; }
        public string DataSource { get => _dataSource; set => _dataSource = value; }
    }
}
