using Application.IRespostory.IAnalysis;
using Domain.Entities.EntityAnalysis;
using Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory
{
    public class RequestSearchHistoryRespostory : IRequestSearchHistoryRespostory
    {
        private AppDbContext _db;

        public RequestSearchHistoryRespostory(AppDbContext db) => _db = db;
        public async Task<bool> Create(RequestSearchHisory dto)
        {
            try
            {
                _db.RequestSearchHisories.Add(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error creating RequestSearchHisory", ex);
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RequestSearchHisory> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RequestSearchHisory>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RequestSearchHisory dto)
        {
            throw new NotImplementedException();
        }
    }
}
