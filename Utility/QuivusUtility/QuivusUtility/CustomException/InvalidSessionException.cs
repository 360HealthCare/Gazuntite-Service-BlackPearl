using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.CustomException
{
    public class InvalidSessionException : Exception
    {
        public int Code { get; set; }

        public InvalidSessionException(string message, int code)
            : base(message)
        {
            Code = code;
        }

        public InvalidSessionException(string message)
            : base(message)
        {
            
        }
    }
}
