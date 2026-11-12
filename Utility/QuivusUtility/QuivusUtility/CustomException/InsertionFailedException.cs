using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.CustomException
{
    public class InsertionFailedException : Exception
    {
        public int Code { get; set; }

        public InsertionFailedException(string message, int code)
            : base(message)
        {
            Code = code;
        }

        public InsertionFailedException(string message)
            : base(message)
        {

        }
    }
}
