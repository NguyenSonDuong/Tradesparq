using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory
{
    public interface IBaseRespostory<T>
    {
        Task<bool> Create(T dto);
        Task<bool> Update(T dto);
        Task<bool> Delete(int id);
        Task<T> Get(int id);
        Task<T> GetAll();

    }
}
