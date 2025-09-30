using Application.IRespostory;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Respostory
{
    public interface IShippentRespostory : IBaseRespostory<Shipment>
    {
        Task<bool> Exit(string shipmentId);
    }
}
