using Domain.Entities.InfoCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory.IInfo
{
    public interface ICityRespostory : IBaseRespostory<City>
    {
        Task<int> CreateAll(List<String> cities);
    }
}
