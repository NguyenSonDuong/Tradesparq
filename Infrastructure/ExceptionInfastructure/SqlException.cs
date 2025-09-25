using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExceptionInfastructure
{
    public class SqlException : Exception
    {
        public SqlException()
        {
        }
        public SqlException(string message)
            : base(message)
        {
        }
        public SqlException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
