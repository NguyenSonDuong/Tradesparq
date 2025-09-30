using Application.IRespostory.IInfo;
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
    public class CityRespostory : ICityRespostory
    {
        private AppDbContext _db;
        public CityRespostory(AppDbContext db) => _db = db;

        public Task<bool> Create(City dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAll(int idCompany,List<string> cities)
        {
            try
            {
                if(cities == null || cities.Count == 0)
                {
                    return 0;
                }
                int count = 0;
                foreach (var city in cities.Distinct())
                {
                    bool exists = await Exits(idCompany,city);
                    if (exists)
                    {
                        continue;
                    }
                    await _db.Cities.AddAsync(new City { CompanyId = idCompany, CityName = city });
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

        public async Task<bool> Exits(int companyId, string city)
        {
            try
            {
                return await _db.Cities.AnyAsync(c => c.CompanyId == companyId && c.CityName == city);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<City> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<City>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(City dto)
        {
            throw new NotImplementedException();
        }
    }
}
