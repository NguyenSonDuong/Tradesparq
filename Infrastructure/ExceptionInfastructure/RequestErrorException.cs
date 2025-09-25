using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExceptionInfastructure
{
    public class RequestErrorException : Exception
    {
        public RequestErrorException()
        {
        }
        public RequestErrorException(string message)
            : base(message)
        {
        }
        public RequestErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
