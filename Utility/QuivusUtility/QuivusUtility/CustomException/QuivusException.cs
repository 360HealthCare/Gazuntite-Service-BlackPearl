using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility.CustomException
{
    public class QuivusException : Exception
    {
        public string Code { get; set; }

        public QuivusException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}