using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
    }
}
