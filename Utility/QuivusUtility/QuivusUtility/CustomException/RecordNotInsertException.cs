using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.CustomException
{
    public class RecordNotInsertException : Exception
    {
        public string Code { get; set; }

        public RecordNotInsertException(string message)
            : base(message)
        {
           
        }

        public RecordNotInsertException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}
