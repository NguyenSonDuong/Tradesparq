using Application.IRespostory.IInfo;
using Domain.Entities;
using Domain.Entities.InfoCompany;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory.Info
{
    public class FaxRespostory : IFaxRespostory
    {
        private readonly AppDbContext _db;
        public FaxRespostory(AppDbContext db) => _db = db;
        public Task<bool> Create(Fax dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAll(int companyId, List<string> faxes)
        {
            try
            {
                if(faxes == null || faxes.Count == 0)
                {
                    return 0;
                }
                int count = 0;
                foreach (var fax in faxes.Distinct())
                {
                    bool exists = await Exits(companyId, fax);
                    if (exists)
                    {
                        continue;
                    }
                    await _db.Faxs.AddAsync(new Fax { CompanyId = companyId, FaxNumber = fax });
                    count++;
                }
                await _db.SaveChangesAsync();
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exits(int companyId, string fax)
        {
            try
            {
                return await _db.Faxs.AnyAsync(c => c.CompanyId == companyId && c.FaxNumber == fax);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Fax> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Fax> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Fax dto)
        {
            throw new NotImplementedException();
        }
    }
}
