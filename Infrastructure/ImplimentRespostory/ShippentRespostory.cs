using Application.Respostory;
using Domain.Entities;
using Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory
{

    public class ShippentRespostory : IShippentRespostory
    {
        private AppDbContext _db;

        public ShippentRespostory(AppDbContext db) => _db = db;
        public async Task<bool> Create(Shipment dto)
        {
            try
            {
                _db.Shipments.Add(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exit(string shipmentId)
        {
            try
            {
                bool isExist = _db.Shipments.Any(s => s.IdShipments == shipmentId);  
                if(!isExist)
                {
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<Shipment> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Shipment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Shipment dto)
        {
            throw new NotImplementedException();
        }
    }
}
