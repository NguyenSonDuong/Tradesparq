using Application.Dto.Keys;
using Application.IRespostory;
using Application.IRespostory.IAuthen;
using Domain.Entities.Authen;
using Infrastructure.DBContext;
using Infrastructure.Helper;
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
                List<AuthenTradesparq> list = await _db.AuthenTradesparqs.Where(x => x.IsDeleted == false && x.Status == StatusAuthen.Active).ToListAsync(); 
                foreach(var item in list)
                {
                    item.Status = StatusAuthen.Disactive;
                    
                }
                _db.AuthenTradesparqs.UpdateRange(list);
                await _db.SaveChangesAsync();
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


        public async Task<List<AuthenTradesparq>> GetAll()
        {
            try
            {
                return await _db.AuthenTradesparqs.Where(x=>x.IsDeleted == false).ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
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

        public async Task<bool> Create(string token, string datasource)
        {
            try
            {
                bool isExit = await _db.AuthenTradesparqs.AnyAsync(c => c.Token == token && c.IsDeleted == false);
                if (isExit)
                {
                    throw new Exception("Create - Error: Token already exists");
                }

                AuthenTradesparq authenTradesparq = new AuthenTradesparq
                {
                    Token = token,
                    dataSourch = datasource,
                    Status = StatusAuthen.Active,
                    IsDeleted = false
                };
                return await Create(authenTradesparq);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Deactive(int id)
        {
            try
            {
                AuthenTradesparq authenTradesparq = await _db.AuthenTradesparqs.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (authenTradesparq == null)
                {
                    throw new Exception($"Deactive - Error: Not found AuthenTradesparq with id {id}");
                }
                authenTradesparq.Status = StatusAuthen.Disactive;
                _db.AuthenTradesparqs.Update(authenTradesparq);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
