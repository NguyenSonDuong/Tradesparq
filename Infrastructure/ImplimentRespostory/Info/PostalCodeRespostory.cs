using Application.IRespostory.IInfo;
using Domain.Entities.InfoCompany;
using Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory.Info
{
    public class PostalCodeRespostory : IPostalCodeRespostory
    {
        private readonly AppDbContext _db;
        public PostalCodeRespostory(AppDbContext db) => _db = db;
        public Task<bool> Create(PostalCode dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAll(int companyId, List<string> postalCodes)
        {
            try
            {
                if(postalCodes == null || postalCodes.Count == 0)
                {
                    return 0;
                }
                int count = 0;
                foreach (var postalCode in postalCodes.Distinct())
                {
                    bool exists = await Exits(companyId, postalCode);
                    if (exists)
                    {
                        continue;
                    }
                    await _db.PostalCodes.AddAsync(new PostalCode { CompanyId = companyId, PostalCodeNumber = postalCode });
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

        public Task<bool> Exits(int companyId, string postalCode)
        {
            throw new NotImplementedException();
        }

        public Task<PostalCode> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostalCode> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PostalCode dto)
        {
            throw new NotImplementedException();
        }
    }
}
