using Domain.Entities.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory.ICommand
{
    public interface ICommandRespostory : IBaseRespostory<Command>
    {
        public Task<Command> GetCommandQueue(); 
        public Task<bool> CloseCommand(int id);
        public Task<int> CreateCommand(Command command);
        public Task<List<Command>> GetAllCommandActive();
    }
}
