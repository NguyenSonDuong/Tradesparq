using Domain.Entities.InfoCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory.IInfo
{
    public interface IPostalCodeRespostory : IBaseRespostory<PostalCode>
    {
        Task<int> CreateAll(int companyId, List<String> postalCodes);
        Task<bool> Exits(int companyId, String postalCode);
    }
}
