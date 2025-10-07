using Application.IRespostory.ICommand;
using Domain.Entities.command;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.ImplimentRespostory.CommandR
{
    public class CommandRespostory : ICommandRespostory
    {
        private AppDbContext _db;

        public CommandRespostory(AppDbContext db) => _db = db;

        public async Task<bool> CloseCommand(int id)
        {
            try
            {
                Command command = await _db.Commands.Where(c => c.Id == id && !c.IsCompleted).FirstOrDefaultAsync();
                if (command == null)
                {
                    throw new Exception($"CloseCommand - Error: Not found command with id {id} or command is closed");
                }
                command.IsCompleted = true;
                _db.Commands.Update(command);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Create(Command dto)
        {
            try
            {
                _db.Commands.Add(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> CreateCommand(Command command)
        {
            try
            {
                _db.Commands.Add(command);
                await _db.SaveChangesAsync();
                return command.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateCommand(string typeCommand, string? comId, string typeSerach, string keySearch, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                if (ToDate < FromDate)
                {
                    throw new Exception("CreateCommand - Error: ToDate must be greater than FromDate");
                }
                bool exists = _db.Commands
                    .Any(c => c.TypeSearch == typeSerach 
                    && c.SearchKey == keySearch 
                    && c.StartDate <= FromDate && c.EndDate >= ToDate 
                    && c.IsDeleted == false);
                Command command;
                if (exists)
                {
                    command = new Command
                    {
                        ComId = comId,
                        TypeCommand = typeCommand,
                        TypeSearch = typeSerach,
                        SearchKey = keySearch,
                        StartDate = FromDate,
                        EndDate = ToDate,
                        CreatedAt = DateTime.UtcNow,
                        IsCompleted = true,
                        IsDeleted = false
                    };
                    _db.Commands.Add(command);
                    await _db.SaveChangesAsync();
                    throw new Exception("CreateCommand - Error: Khoảng thời gian bạn tìm kiếm đã có dữ liệu! Vui lòng xác nhận với kỹ thuật viên để có thể chạy lệnh - Lệnh đã được lưu chờ quản trị viên duyệt");
                }
                command = new Command
                {
                    TypeCommand = typeCommand,
                    TypeSearch = typeSerach,
                    SearchKey = keySearch,
                    StartDate = FromDate,
                    EndDate = ToDate,
                    CreatedAt = DateTime.UtcNow,
                    IsCompleted = false,
                    IsDeleted = false
                };
                if (!string.IsNullOrEmpty(comId))
                {
                    command.ComId = comId;
                }
                _db.Commands.Add(command);
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
                Command command = await _db.Commands.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync();
                command.IsDeleted = true;
                _db.Commands.Update(command);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Command> Get(int id)
        {
            try
            {
                var command = await _db.Commands.AsNoTracking().Where(c => c.Id == id).ToListAsync();
                return null;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Command>> GetAll()
        {
            try
            {
                List<Command> listCommand = await _db.Commands.AsNoTracking().ToListAsync();
                return listCommand;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Command>> GetAllCommandActive()
        {
            try
            {
                List<Command> commands = await _db.Commands
                    .Where(c => !c.IsCompleted && !c.IsDeleted)
                    .OrderBy(c => c.CreatedAt)
                    .ToListAsync();
                return commands;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Command> GetCommandQueue()
        {
            try
            {
                Command command = await _db.Commands.Where(c => c.IsCompleted == false).OrderBy(c => c.CreatedAt).FirstOrDefaultAsync();
                return command;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Update(Command dto)
        {
            throw new NotImplementedException();
        }
    }
}
