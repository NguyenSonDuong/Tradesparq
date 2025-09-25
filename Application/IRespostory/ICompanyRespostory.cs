using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory
{
    public interface ICompanyRespostory : IBaseRespostory<Company>
    {
        Task<Company> Get(String companyId);
        Task<bool> Exits(string uuid);

    }
}
