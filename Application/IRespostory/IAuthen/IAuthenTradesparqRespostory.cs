using Domain.Entities.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRespostory.IAuthen
{
    public interface IAuthenTradesparqRespostory : IBaseRespostory<AuthenTradesparq>
    {
        Task<AuthenTradesparq> GetTokenActive();
    }
}
