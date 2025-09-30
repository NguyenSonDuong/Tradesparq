using Application.IRespostory;
using Domain.Entities;
using Infrastructure.DBContext;
using Infrastructure.ExceptionInfastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory
{
    public class CompanyRespostory : ICompanyRespostory
    {
        private AppDbContext _db;

        public CompanyRespostory(AppDbContext db) => _db = db;

        public async Task<bool> Create(Company dto)
        {
            try
            {
                _db.Companies.Add(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Company company = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id);
                if (company == null)
                {
                    throw new SqlException($"Delete - Error: Not found company with id {id}");
                }
                _db.Companies.Remove(company);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Exits(string uuid)
        {
            try
            {
                Company company = await _db.Companies.FirstOrDefaultAsync(c => c.Uuid == uuid);
                if (company == null)
                {
                   return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Company> Get(string uuid)
        {
            try
            {
                Company company = await _db.Companies.FirstOrDefaultAsync(c => c.Uuid == uuid);
                if (company == null)
                {
                    throw new SqlException($"Delete - Error: Not found company with id {uuid}");
                }
                return company;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Company> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Company>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyDetailsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Company dto)
        {
            throw new NotImplementedException();
        }
    }
}
