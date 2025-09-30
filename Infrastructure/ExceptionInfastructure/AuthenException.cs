using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExceptionInfastructure
{
    public class AuthenException : Exception
    {
        public AuthenException()
        {
        }
        public AuthenException(string message)
            : base(message)
        {
        }
        public AuthenException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
