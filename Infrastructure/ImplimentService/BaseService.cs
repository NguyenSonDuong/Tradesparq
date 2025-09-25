using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentService
{
    public abstract  class BaseService : IService
    {
        private string _token;
        private string _dataSource;
        public string Token { get => this._token; set => this._token = value; }
        public string DataSource { get => this._dataSource; set => this._dataSource = value; }
    }
}
