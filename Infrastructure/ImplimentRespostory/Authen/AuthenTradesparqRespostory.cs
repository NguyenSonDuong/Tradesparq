using Application.Dto.Keys;
using Application.IRespostory;
using Application.IRespostory.IAuthen;
using Domain.Entities.Authen;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentRespostory.Authen
{
    public class AuthenTradesparqRespostory : IAuthenTradesparqRespostory
    {
        private AppDbContext _db;
        public AuthenTradesparqRespostory(AppDbContext db) => _db = db;
        public async Task<bool> Create(AuthenTradesparq dto)
        {
            try
            {
                _db.AuthenTradesparqs.Add(dto);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenTradesparq> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(AuthenTradesparq dto)
        {
            throw new NotImplementedException();
        }


        public Task<List<AuthenTradesparq>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenTradesparq> GetTokenActive()
        {
            try
            {
                return await _db.AuthenTradesparqs.Where(c => c.Status == StatusAuthen.Active).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw;
            }   
        }
    }
}
