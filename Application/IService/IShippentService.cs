using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IShippentService
    {
        Task<ShipmentAnalysisDto> SaveAllShipment(SearchRequestDto.Root searchRequestDto);
        Task<ShipmentAnalysisDto> SaveShipmentOfCompany(CompanySearchRequestDto companySearchRequestDto);
    }
}
