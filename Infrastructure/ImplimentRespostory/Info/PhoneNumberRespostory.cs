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
    public class PhoneNumberRespostory : IPhoneNumberRespostory
    {
        private readonly AppDbContext _db;
        public PhoneNumberRespostory(AppDbContext db) => _db = db;
        public Task<bool> Create(PhoneNumber dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAll(int companyId, List<string> phoneNumbers)
        {
            try
            {
                if (phoneNumbers == null || phoneNumbers.Count == 0)
                {
                    return 0;
                }
                int count = 0;
                foreach (var phoneNumber in phoneNumbers.Distinct())
                {
                    bool exists = await Exits(companyId, phoneNumber);
                    if (exists)
                    {
                        continue;
                    }
                    await _db.PhoneNumbers.AddAsync(new PhoneNumber { CompanyId = companyId, PhoneNum = phoneNumber });
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

        public async Task<bool> Exits(int companyId, string phonenumber)
        {
            try
            {
                return await _db.PhoneNumbers.AnyAsync(c => c.CompanyId == companyId && c.PhoneNum == phonenumber);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhoneNumber> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PhoneNumber> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PhoneNumber dto)
        {
            throw new NotImplementedException();
        }
    }
}
