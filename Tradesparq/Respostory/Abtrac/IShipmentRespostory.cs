using CrawlService.Dto.Responsive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradesparq.Respostory.Abtrac
{
    public interface IShipmentRespostory
    {
        Task<bool> Create(SearchResponsiveDto.Root searchResponsiveDto);
        Task<bool> Update(SearchResponsiveDto.Root searchResponsiveDto);
        Task<bool> Delete(string id);
        Task<bool> IsExist(string id);
        Task<bool> GetAll();
        Task<bool> Get(string id);
    }
}
