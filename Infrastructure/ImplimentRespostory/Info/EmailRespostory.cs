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
    public class EmailRespostory : IEmailRespostory
    {
        private readonly AppDbContext _db;
        public EmailRespostory(AppDbContext db) => _db = db;
        public Task<bool> Create(Email dto)
        {
            throw new NotImplementedException();
        }


        public async Task<int> CreateAll(int idCompany, List<string> emails)
        {
            try
            {
                if (emails == null || emails.Count == 0)
                {
                    return 0;
                }
                int count = 0;
                foreach (var email in emails.Distinct())
                {
                    bool exists = await Exits(idCompany, email);
                    if (exists)
                    {
                        continue;
                    }
                    await _db.Emails.AddAsync(new Email { CompanyId = idCompany, EmailAddress = email });
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

        public async Task<bool> Exits(int companyId, string email)
        {
            try
            {
                return await _db.Emails.AnyAsync(c => c.CompanyId == companyId && c.EmailAddress == email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Email> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Email> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Email dto)
        {
            throw new NotImplementedException();
        }
    }
}
