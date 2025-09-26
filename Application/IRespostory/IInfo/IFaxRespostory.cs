using Domain.Entities.InfoCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory.IInfo
{
    public interface IFaxRespostory : IBaseRespostory<Fax>
    {
        Task<int> CreateAll(int companyId, List<String> faxes);
        Task<bool> Exits(int companyId, String fax);
    }
}
