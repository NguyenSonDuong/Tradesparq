using AutoMapper;
using CrawlService.Dto.Responsive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradesparq.Model.Company;
using Tradesparq.Respostory.Abtrac;
using TradesparqDBContext;

namespace Tradesparq.Respostory
{
    public class ShipmentRespostory : IShipmentRespostory
    {
        protected readonly AppDbContext _db;
        protected IMapper _mapper;
        public ShipmentRespostory(AppDbContext dbContext , IMapper _mapper)
        {
            _db = dbContext;
            this._mapper = _mapper;
        }
        public Task<bool> Create(SearchResponsiveDto.Root searchResponsiveDto)
        {
            try
            {
                if(searchResponsiveDto.code != 200000)
                {
                    throw new Exception("Error from api");
                }
                foreach (var item in searchResponsiveDto.data.docs)
                {
                    bool IsHas = _db.Shipment.Any(x => x.IdShipments == item.id);
                    if (IsHas)
                    {
                        throw new Exception("Data Already Exist");
                    }
                    _db.Shipment.Add(_mapper.Map<Shipments>(item));
                }

                return Task.FromResult(true);
                   
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SearchResponsiveDto.Root searchResponsiveDto)
        {
            throw new NotImplementedException();
        }
    }
}
