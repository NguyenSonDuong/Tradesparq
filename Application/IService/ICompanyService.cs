using Application.Dto.Request;
using Application.Dto.ResponsiveDto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICompanyService : IService
    {
        Task<int> GetAllCompany(SearchRequestDto.Root searchRequestDto);
    }
}
