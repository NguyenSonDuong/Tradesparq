using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.InfoCompany
{
    public class PostalCode
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int TypeInfo { get; set; }
        public string PostalCodeNumber { get; set; }
    }
}
